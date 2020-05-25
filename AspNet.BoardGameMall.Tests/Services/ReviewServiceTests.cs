using Portfolio.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.BoardGameMall.Tests
{
    [TestClass]
    public class ReviewServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

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

            var reviews = new List<Review>
            {
                new Review{ ReviewId = 95, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(4) },
                new Review{ ReviewId = 96, ProductId = 1, RefLevel = 2, RefId = 99, InsertDt = DateTime.Now.AddMinutes(3) },
                new Review{ ReviewId = 97, ProductId = 1, RefLevel = 2, RefId = 98, InsertDt = DateTime.Now.AddMinutes(2) },
                new Review{ ReviewId = 98, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(1) },
                new Review{ ReviewId = 99, ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now },
                new Review{ ReviewId = 100, ProductId = 1, RefLevel = 0, InsertDt = DateTime.Now },
                new Review{ ReviewId = 199, ProductId = 1, RefLevel = 1, RefId = 200, InsertDt = DateTime.Now },
                new Review{ ReviewId = 200, ProductId = 1, RefLevel = 0, InsertDt = DateTime.Now },
                new Review{ ReviewId = 300, ProductId = 2, RefLevel = 0, InsertDt = DateTime.Now },
            };

            /* ReveiwId 에 따른 Reveiw 테스트 구조
             * 300
             * 200 - 199
             * 100 - 99 - 96
             *     ┗ 98 - 97
             *     ┗ 95
             */

            reviews.ForEach(x =>
            {
                x.Title = "제목";
                x.UserName = "테스트유저";
                x.UserId = "c8429f19";
                x.Content = "내용";
            });

            context.Products.AddRange(products);
            context.ImageUseTypes.AddRange(imageUseTypes);
            context.ProductImages.Add(productImage);
            context.Reviews.AddRange(reviews);

            context.SaveChanges();
        }

        [TestMethod]
        public void GetList()
        {
            long productId = 1;
            int pageNumber = 1;
            int pageSize = 5;

            /* ReveiwId 에 따른 Reveiw 테스트 구조
             * 200 - 199
             * 100 - 99 - 96
             *     ┗ 98 - 97
             *     ┗ 95
             */

            var service = new ReviewService(context);
            var result = service.GetList(productId, pageNumber, pageSize);
            var list = result.Reviews.ToList();
            var pagenation = result.Pagenation;

            Assert.AreEqual(pageNumber, pagenation.PageNumber);
            Assert.AreEqual(pageSize, pagenation.PageSize);
            Assert.AreEqual(1, pagenation.PageCount);
            Assert.AreEqual(2, pagenation.Count);

            Assert.AreEqual(8, list.Count);
            Assert.AreEqual(200, list[0].ReviewId);
            Assert.AreEqual(199, list[1].ReviewId);
            Assert.AreEqual(100, list[2].ReviewId);
            Assert.AreEqual(99, list[3].ReviewId);
            Assert.AreEqual(96, list[4].ReviewId);
            Assert.AreEqual(98, list[5].ReviewId);
            Assert.AreEqual(97, list[6].ReviewId);
            Assert.AreEqual(95, list[7].ReviewId);
        }

        [TestMethod]
        public void ReviewAdd()
        {
            /* ReviewId 에 따른 Review 테스트 구조 ()안은 테스트로 추가할 Review
             * (400)
             * 300
             * 200 -  199 - (198)
             * 100 -  99  -  96  - (94)
             *     ┗  98  -  97
             *     ┗  95
             *     ┗ (93)
             */

            Review review400 = new Review { ProductId = 1, RefLevel = 0, InsertDt = DateTime.Now.AddMinutes(-5), Title = "제목", Content = "내용", UserName = "테스트유저", UserId = "c8429f19" };
            Review review100_1_1_1 = new Review { ProductId = 1, RefLevel = 3, RefId = 96, InsertDt = DateTime.Now.AddMinutes(-4), Title = "제목", Content = "내용", UserName = "테스트유저", UserId = "c8429f19" };
            Review review100_4 = new Review { ProductId = 1, RefLevel = 1, RefId = 100, InsertDt = DateTime.Now.AddMinutes(-3), Title = "제목", Content = "내용", UserName = "테스트유저", UserId = "c8429f19" };
            Review review200_1_1 = new Review { ProductId = 1, RefLevel = 2, RefId = 199, InsertDt = DateTime.Now.AddMinutes(-2), Title = "제목", Content = "내용", UserName = "테스트유저", UserId = "c8429f19" };

            var service = new ReviewService(context);
            service.ReviewAdd(review400);
            service.ReviewAdd(review100_1_1_1);
            service.ReviewAdd(review100_4);
            service.ReviewAdd(review200_1_1);

            Assert.AreEqual(400, review400.ReviewId);
            Assert.AreEqual(94, review100_1_1_1.ReviewId);
            Assert.AreEqual(93, review100_4.ReviewId);
            Assert.AreEqual(198, review200_1_1.ReviewId);
        }

        [TestMethod]
        public void View()
        {
            long reviewId = 100;

            var service = new ReviewService(context);
            var review100 = service.View(reviewId);

            Assert.AreNotEqual(null, review100);
            Assert.AreNotEqual(null, review100.Product);
            Assert.AreEqual(0, review100.RefLevel);
            Assert.AreEqual(null, review100.RefId);
            Assert.AreEqual(review100.ProductId, review100.Product.ProductId);
            Assert.AreEqual("첫번째 상품", review100.Product.ProductName);
            Assert.AreEqual(10000, review100.Product.Price);
            Assert.AreEqual("~/Images/Products/1_T1.png", review100.Product.ProductMainImagePath);

            reviewId = 300;
            var review300 = service.View(reviewId);

            Assert.AreNotEqual(null, review300);
            Assert.AreNotEqual(null, review300.Product);
            Assert.AreEqual(0, review300.RefLevel);
            Assert.AreEqual(null, review300.RefId);
            Assert.AreEqual(review300.ProductId, review300.Product.ProductId);
            Assert.AreEqual("두번째 상품", review300.Product.ProductName);
            Assert.AreEqual(20000, review300.Product.Price);
            Assert.AreEqual(18000, review300.Product.PromotionPrice);
            Assert.AreEqual(StringConst.EmptyImagePath, review300.Product.ProductMainImagePath);
        }

        [TestMethod]
        public void Update()
        {
            Review modified = new Review
            {
                ReviewId = 300,
                ProductId = 1,
                Rating = 4.5m,
                RefLevel = 0,
                InsertDt = new DateTime(2020, 1, 1, 1, 0, 0),
                UpdateDt = DateTime.Now,
                UserName = "테스트유저",
                Title = "수정한 제목",
                Content = "수정한 내용"
            };

            var service = new ReviewService(context);

            service.Update(modified);

            var updatedReview = context.Reviews.First(x => x.ReviewId == 300);
            Assert.AreEqual("수정한 제목", updatedReview.Title);
            Assert.AreEqual("수정한 내용", updatedReview.Content);
            Assert.AreEqual(4.5m, updatedReview.Rating);
        }

        [TestMethod]
        public void Delete()
        {
            long reviewId = 100;

            var service = new ReviewService(context);

            try
            {
                service.Delete(reviewId, "8e31988d");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("작성자만 리뷰를 삭제할 수 있습니다.", ex.Message);
                Assert.AreNotEqual(null, context.Reviews.FirstOrDefault(x => x.ReviewId == reviewId));
            }

            service.Delete(reviewId, "c8429f19");

            var review100 = context.Reviews.FirstOrDefault(x => x.ReviewId == reviewId);
            Assert.AreEqual(null, review100);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            long reviewId = 100;

            var service = new ReviewService(context);

            service.DeleteAdmin(reviewId);

            var review100 = context.Reviews.FirstOrDefault(x => x.ReviewId == reviewId);
            Assert.AreEqual(null, review100);
        }
    }
}
