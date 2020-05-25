using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
using Portfolio.Services.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AspNet.BoardGameMall.Tests
{
    [TestClass]
    public class InquiryServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            this.context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var products = new List<Product>
            {
                new Product{ ProductId = 1, CategoryId = 1, ProductName = "첫번째 상품", Price = 10000 },
                new Product{ ProductId = 2, CategoryId = 2, ProductName = "두번째 상품", Price = 20000, PromotionPrice = 18000 }
            };

            var imageUseTypes = new List<ImageUseType>();
            foreach (ImageUseTypeEnum item in Enum.GetValues(typeof(ImageUseTypeEnum)))
            {
                imageUseTypes.Add(new ImageUseType { ImageUseTypeId = (int)item, TypeName = item.ToString() });
            }

            var productImage = new ProductImage
            {
                ProductId = 1,
                ImageName = "FirstImage.png",
                ImageUseTypeId = (int)ImageUseTypeEnum.메인섬네일,
                ImagePath = "~/Images/Products/1_T1.png",
                ImageSize = 1024,
                ImageType = "png",
                InsertDt = DateTime.Now
            };

            var inquiries = new List<Inquiry>
            {
                new Inquiry{ InquiryId = 95, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(4) },
                new Inquiry{ InquiryId = 96, ProductId = 1, RefLevel = 2, RefId = 99, InsertDt = DateTime.Now.AddMinutes(3) },
                new Inquiry{ InquiryId = 97, ProductId = 1, RefLevel = 2, RefId = 98, InsertDt = DateTime.Now.AddMinutes(2) },
                new Inquiry{ InquiryId = 98, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(1) },
                new Inquiry{ InquiryId = 99, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now },
                new Inquiry{ InquiryId = 100, ProductId = 1, RefLevel = 0, InsertDt = DateTime.Now },
                new Inquiry{ InquiryId = 199, ProductId = 1, RefLevel = 1, RefId = 200, InsertDt = DateTime.Now },
                new Inquiry{ InquiryId = 200, ProductId = 1, RefLevel = 0, InsertDt = DateTime.Now },
                new Inquiry{ InquiryId = 300, ProductId = 2, RefLevel = 0, InsertDt = DateTime.Now }
            };

            /* InquiryId 에 따른 Inquiry 테스트 구조
             * 300
             * 200 - 199
             * 100 - 99 - 96
             *     ┗ 98 - 97
             *     ┗ 95
             */

            inquiries.ForEach(x =>
            {
                x.IsLocked = false;
                x.Title = "제목";
                x.UserName = "테스트유저";
                x.UserId = "c8429f19";
                x.Content = "내용";
            });

            context.Products.AddRange(products);
            context.ImageUseTypes.AddRange(imageUseTypes);
            context.ProductImages.Add(productImage);
            context.Inquiries.AddRange(inquiries);

            context.SaveChanges();
        }

        //질문게시판에서 해당되는 상품의 질문/답변을 답글 순서에 맞춰 제대로 가져오는지 테스트
        [TestMethod]
        public void GetList()
        {
            long productId = 1;
            int pageNumber = 1;
            int pageSize = 5;
            
            /* InquiryId 에 따른 Inquiry 테스트 구조
             * 200 - 199
             * 100 - 99 - 96
             *     ┗ 98 - 97
             *     ┗ 95
             */

            var service = new InquiryService(context);
            var result = service.GetList(productId, pageNumber, pageSize);
            var list = result.Inquiries.ToList();
            var pagenation = result.Pagenation;

            Assert.AreEqual(pageNumber, pagenation.PageNumber);
            Assert.AreEqual(pageSize, pagenation.PageSize);
            Assert.AreEqual(1, pagenation.PageCount);
            Assert.AreEqual(2, pagenation.Count);

            Assert.AreEqual(8, list.Count);
            Assert.AreEqual(200, list[0].InquiryId);
            Assert.AreEqual(199, list[1].InquiryId);
            Assert.AreEqual(100, list[2].InquiryId);
            Assert.AreEqual(99, list[3].InquiryId);
            Assert.AreEqual(96, list[4].InquiryId);
            Assert.AreEqual(98, list[5].InquiryId);
            Assert.AreEqual(97, list[6].InquiryId);
            Assert.AreEqual(95, list[7].InquiryId);
        }

        //질문게시판에서 질문/답변을 등록시에 InquiryId가 질문은 100 단위 증가, 답변은 해당 질문의 InquiryId에서 1씩 감소여부 테스트
        [TestMethod]
        public void InquiryAdd()
        {
            /* InquiryId 에 따른 Inquiry 테스트 구조 ()안은 테스트로 추가할 Inquiry
             * (300)
             *  200  -  199
             *  100  -  99 - (96)
             *           ┗   (94)
             *       ┗  98 -  97
             *       ┗ (95)
             */

            Product product = new Product { ProductId = 1, CategoryId = 1, ProductName = "첫번째 상품", Price = 10000 };
            
            var inquiries = new List<Inquiry>
            {
                new Inquiry{ InquiryId = 97, ProductId = 1, RefLevel = 2, RefId = 98, InsertDt = DateTime.Now.AddMinutes(-3) },
                new Inquiry{ InquiryId = 98, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(-4) },
                new Inquiry{ InquiryId = 99, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(-5) },
                new Inquiry{ InquiryId = 100, ProductId = 1, RefLevel = 0 },
                new Inquiry{ InquiryId = 199, ProductId = 1, RefLevel = 1, RefId = 200 },
                new Inquiry{ InquiryId = 200, ProductId = 1, RefLevel = 0 }
            };

            inquiries.ForEach(x =>
            {
                x.IsLocked = false;
                x.Title = "제목";
                x.UserName = "테스트유저";
                x.Content = "내용";
            });

            using (var context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient()))
            {
                context.Products.Add(product);
                context.Inquiries.AddRange(inquiries);
                context.SaveChanges();

                var question = new Inquiry
                {
                    ProductId = 1,
                    RefLevel = 0,
                    InsertDt = DateTime.Now,
                    IsLocked = false,
                    Title = "제목",
                    UserName = "테스트유저",
                    Content = "내용"
                };
                var answer_1_1 = new Inquiry
                {
                    ProductId = 1,
                    RefLevel = 2,
                    RefId = 99,
                    InsertDt = DateTime.Now.AddSeconds(1),
                    IsLocked = false,
                    Title = "제목",
                    UserName = "테스트유저",
                    Content = "내용"
                };
                var answer_3 = new Inquiry
                {
                    ProductId = 1,
                    RefLevel = 1,
                    RefId = 100,
                    InsertDt = DateTime.Now.AddSeconds(2),
                    IsLocked = false,
                    Title = "제목",
                    UserName = "테스트유저",
                    Content = "내용"
                };
                var answer_1_2 = new Inquiry
                {
                    ProductId = 1,
                    RefLevel = 2,
                    RefId = 99,
                    InsertDt = DateTime.Now.AddSeconds(3),
                    IsLocked = false,
                    Title = "제목",
                    UserName = "테스트유저",
                    Content = "내용"
                };

                var service = new InquiryService(context);
                service.InquiryAdd(question);
                service.InquiryAdd(answer_1_1);
                service.InquiryAdd(answer_3);
                service.InquiryAdd(answer_1_2);

                Assert.AreEqual(300, question.InquiryId);
                Assert.AreEqual(96, answer_1_1.InquiryId);
                Assert.AreEqual(95, answer_3.InquiryId);
                Assert.AreEqual(94, answer_1_2.InquiryId);
            }
        }

        //질문게시판에서 id에 해당하는 문의글을 가져오는지 테스트
        [TestMethod]
        public void View()
        {
            var service = new InquiryService(context);

            var inquiry100 = service.View(100);
            
            Assert.AreNotEqual(null, inquiry100);
            Assert.AreNotEqual(null, inquiry100.Product);
            Assert.AreEqual(0, inquiry100.RefLevel);
            Assert.AreEqual(null, inquiry100.RefId);
            Assert.AreEqual(inquiry100.ProductId, inquiry100.Product.ProductId);
            Assert.AreEqual("첫번째 상품", inquiry100.Product.ProductName);
            Assert.AreEqual(10000, inquiry100.Product.Price);
            Assert.AreEqual("~/Images/Products/1_T1.png", inquiry100.Product.ProductMainImagePath);

            var inquiry300 = service.View(300);

            Assert.AreNotEqual(null, inquiry300);
            Assert.AreNotEqual(null, inquiry300.Product);
            Assert.AreEqual(0, inquiry300.RefLevel);
            Assert.AreEqual(null, inquiry300.RefId);
            Assert.AreEqual(inquiry300.ProductId, inquiry300.Product.ProductId);
            Assert.AreEqual("두번째 상품", inquiry300.Product.ProductName);
            Assert.AreEqual(20000, inquiry300.Product.Price);
            Assert.AreEqual(18000, inquiry300.Product.PromotionPrice);
            Assert.AreEqual(StringConst.EmptyImagePath, inquiry300.Product.ProductMainImagePath);
        }

        //문의글 수정(뷰에서 제목, 내용, 잠금여부만 수정가능)
        [TestMethod]
        public void Update()
        {
            using (var context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient()))
            {
                Product product = new Product
                {
                    ProductId = 1,
                    CategoryId = 2,
                    ProductName = "두번째 상품",
                    Price = 20000,
                    PromotionPrice = 18000
                };

                Inquiry inquiry = new Inquiry
                {
                    InquiryId = 100,
                    ProductId = 1,
                    RefLevel = 0,
                    InsertDt = new DateTime(2020, 1, 1, 1, 0, 0),
                    IsLocked = false,
                    Title = "제목",
                    UserName = "테스트유저",
                    Content = "내용"
                };

                context.Products.Add(product);
                context.Inquiries.Add(inquiry);
                context.SaveChanges();

                var service = new InquiryService(context);

                Inquiry modified = new Inquiry
                {
                    InquiryId = 100,
                    ProductId = 1,
                    RefLevel = 0,
                    InsertDt = new DateTime(2020, 1, 1, 1, 0, 0),
                    UpdateDt = DateTime.Now,
                    UserName = "테스트유저",
                    Title = "수정한 제목",
                    Content = "수정한 내용",
                    IsLocked = true
                };

                service.Update(modified);

                var updatedInquiry = context.Inquiries.First(x => x.InquiryId == 100);

                Assert.AreEqual("수정한 제목", updatedInquiry.Title);
                Assert.AreEqual("수정한 내용", updatedInquiry.Content);
                Assert.AreEqual(true, updatedInquiry.IsLocked);
            }
        }
        
        [TestMethod]
        public void Delete()
        {
            long inquiryId = 100;

            var service = new InquiryService(context);

            try
            {                
                service.Delete(inquiryId, "8e31988d");
            }
            catch(Exception ex)
            {
                Assert.AreEqual("작성자만 삭제할 수 있습니다.", ex.Message);
                Assert.AreNotEqual(null, context.Inquiries.FirstOrDefault(x => x.InquiryId == inquiryId));
            }

            service.Delete(inquiryId, "c8429f19");

            var inquiry100 = context.Inquiries.FirstOrDefault(x => x.InquiryId == inquiryId);
            Assert.AreEqual(null, inquiry100);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            long inquiryId = 100;

            var service = new InquiryService(context);
            
            service.DeleteAdmin(inquiryId);

            var inquiry100 = context.Inquiries.FirstOrDefault(x => x.InquiryId == inquiryId);
            Assert.AreEqual(null, inquiry100);
        }

    }
}