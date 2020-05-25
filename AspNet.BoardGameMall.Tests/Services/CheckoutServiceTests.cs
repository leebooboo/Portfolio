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
    public class CheckoutServiceTests
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

            var checkouts = new List<Checkout>
            {
                new Checkout{ UserId = "c8429f19", ProductId = 1, ProductCount = 3, InsertDt = DateTime.Now.AddSeconds(-3) },
                new Checkout{ UserId = "c8429f19", ProductId = 2, ProductCount = 10, InsertDt = DateTime.Now.AddSeconds(-2) },
                new Checkout{ UserId = "8e31988d", ProductId = 1, ProductCount = 2, InsertDt = DateTime.Now.AddSeconds(-1) },
            };

            context.Products.AddRange(products);
            context.ImageUseTypes.AddRange(imageUseTypes);
            context.ProductImages.Add(productImage);
            context.Checkouts.AddRange(checkouts);

            context.SaveChanges();
        }

        [TestMethod]
        public void GetList()
        {
            string userId = "c8429f19";

            var service = new CheckoutService(context);

            #region IEnumerable<CheckoutItemDto> GetList(string userId) 테스트
            var checkoutList_UserId = service.GetList(userId).ToList();

            Assert.AreEqual(2, checkoutList_UserId.Count);
            Assert.AreEqual(1, checkoutList_UserId[0].ProductId);
            Assert.AreEqual(2, checkoutList_UserId[1].ProductId);
            Assert.AreEqual(3, checkoutList_UserId[0].ProductCount);
            Assert.AreEqual("첫번째 상품", checkoutList_UserId[0].ProductName);
            Assert.AreEqual("~/Images/Products/1_T1.png", checkoutList_UserId[0].MainImagePath);
            Assert.AreEqual(StringConst.EmptyImagePath, checkoutList_UserId[1].MainImagePath);
            Assert.AreEqual(10000, checkoutList_UserId[0].Price);
            Assert.AreEqual(18000, checkoutList_UserId[1].Price);
            #endregion

            #region IEnumerable<CheckoutItemDto> GetList(IEnumerable<Checkout> checkoutList) 테스트
            var checkoutList = new List<Checkout>
            {
                new Checkout{ ProductId = 2, ProductCount = 5 },
                new Checkout{ ProductId = 1, ProductCount = 7 }
            };

            var checkoutResult = service.GetList(checkoutList).ToList();

            Assert.AreEqual(2, checkoutResult.Count);
            Assert.AreEqual(1, checkoutResult[0].ProductId);
            Assert.AreEqual(2, checkoutResult[1].ProductId);
            Assert.AreEqual("첫번째 상품", checkoutResult[0].ProductName);
            Assert.AreEqual(7, checkoutResult[0].ProductCount);
            Assert.AreEqual(5, checkoutResult[1].ProductCount);
            Assert.AreEqual("~/Images/Products/1_T1.png", checkoutResult[0].MainImagePath);
            Assert.AreEqual(StringConst.EmptyImagePath, checkoutResult[1].MainImagePath);
            Assert.AreEqual(10000, checkoutResult[0].Price);
            Assert.AreEqual(18000, checkoutResult[1].Price);
            #endregion
        }

        [TestMethod]
        public void UpsertItem()
        {
            var service = new CheckoutService(context);

            string userId = "c8429f19";
            long productId = 1;
            int productCount = 11;

            service.UpsertItem(userId, productId, productCount); //update 처리

            var result1 = context.Checkouts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            Assert.AreNotEqual(null, result1);
            Assert.AreEqual(productId, result1.ProductId);
            Assert.AreEqual(productCount, result1.ProductCount);

            productId = 3;
            productCount = 4;

            service.UpsertItem(userId, productId, productCount); //insert 처리

            var result2 = context.Checkouts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            Assert.AreNotEqual(null, result2);
            Assert.AreEqual(productId, result2.ProductId);
            Assert.AreEqual(productCount, result2.ProductCount);
        }

        [TestMethod]
        public void InsertItems()
        {
            var checkoutList = new List<Checkout>
            {
                new Checkout{ ProductId = 6, ProductCount = 4 },
                new Checkout{ ProductId = 5, ProductCount = 6 }
            };

            var service = new CheckoutService(context);
            string userId = "8e31988d";
            service.InsertItems(userId, checkoutList);

            var result = context.Checkouts.Where(x => x.UserId == userId).OrderBy(x => x.ProductId).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(5, result[1].ProductId);
            Assert.AreEqual(6, result[2].ProductId);
            Assert.AreEqual(6, result[1].ProductCount);
            Assert.AreEqual(4, result[2].ProductCount);
        }

        [TestMethod]
        public void DeleteItems()
        {
            var productIdList = new List<long> { 1, 2 };

            var service = new CheckoutService(context);
            string userId = "c8429f19";
            service.DeleteItems(userId, productIdList);

            var result = context.Checkouts.Where(x => x.UserId == userId).ToList();
            Assert.AreEqual(0, result.Count);            
        }
    }
}
