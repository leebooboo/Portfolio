using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Portfolio.Entities.Models;
using System.Data.Entity;
using Portfolio.Entities;
using Portfolio.Services.DTO;
using System.Diagnostics;
using Portfolio.Entities.Enums;

namespace AspNet.BoardGameMall.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            this.context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var imageUseTypes = new List<ImageUseType>();
            foreach (ImageUseTypeEnum item in Enum.GetValues(typeof(ImageUseTypeEnum)))
            {
                imageUseTypes.Add(new ImageUseType { ImageUseTypeId = (int)item, TypeName = item.ToString() });
            }

            var products = new List<Product>
            {
                new Product{
                    ProductId = 1,
                    CategoryId = 2,
                    ProductName = "첫번째 상품",
                    Price = 10000
                },
                new Product{
                    ProductId = 2,
                    CategoryId = 1,
                    ProductName = "2번 상품",
                    Price = 20000
                },
                new Product{
                    ProductId = 3,
                    CategoryId = 2,
                    ProductName = "3번 상품",
                    Price = 30000
                },
            };

            var productImages = new List<ProductImage>
            {
                new ProductImage
                {
                    ProductImageId = 1,
                    ProductId = 1,
                    ImageUseTypeId = 3,
                    ImageName = "101.jpg",
                    ImageType = "jpg",
                    ImagePath = "~/Images/1_T3_1.jpg"
                },
                new ProductImage
                {
                    ProductImageId = 2,
                    ProductId = 2,
                    ImageUseTypeId = 1,
                    ImageName = "102.png",
                    ImageType = "png",
                    ImagePath = "~/Images/2_T1_1.png"
                },
                new ProductImage
                {
                    ProductImageId = 3,
                    ProductId = 1,
                    ImageUseTypeId = 1,
                    ImageName = "103.png",
                    ImageType = "png",
                    ImagePath = "~/Images/1_T1_1.png"
                }
            };

            context.ImageUseTypes.AddRange(imageUseTypes);
            context.Products.AddRange(products);
            context.ProductImages.AddRange(productImages);
            context.SaveChanges();
        }
        
        [TestMethod]
        public void GetProducts()
        {            
            var service = new ProductService(context);

            var resultCategory = service.GetCategoryProducts(2).ToList();

            Assert.AreEqual(2, resultCategory.Count());
            Assert.AreEqual("첫번째 상품", resultCategory[0].ProductName);
            Assert.AreEqual("3번 상품", resultCategory[1].ProductName);
            Assert.AreEqual("~/Images/1_T1_1.png", resultCategory[0].ProductMainImagePath);
            Assert.AreEqual(StringConst.EmptyImagePath, resultCategory[1].ProductMainImagePath);

            var resultProductName = service.GetProducts("번 상품").ToList();

            Assert.AreEqual(2, resultProductName.Count());
            Assert.AreEqual("2번 상품", resultProductName[0].ProductName);
            Assert.AreEqual("3번 상품", resultProductName[1].ProductName);
            Assert.AreEqual("~/Images/2_T1_1.png", resultProductName[0].ProductMainImagePath);
            Assert.AreEqual(StringConst.EmptyImagePath, resultProductName[1].ProductMainImagePath);

            var resultPaging20 = service.GetProducts(1, 20).ToList();

            Assert.AreEqual(3, resultPaging20.Count());
            Assert.AreEqual("첫번째 상품", resultPaging20[0].ProductName);
            Assert.AreEqual("2번 상품", resultPaging20[1].ProductName);
            Assert.AreEqual("3번 상품", resultPaging20[2].ProductName);
            Assert.AreEqual("~/Images/1_T1_1.png", resultPaging20[0].ProductMainImagePath);
            Assert.AreEqual("~/Images/2_T1_1.png", resultPaging20[1].ProductMainImagePath);
            Assert.AreEqual(StringConst.EmptyImagePath, resultPaging20[2].ProductMainImagePath);

            var resultPaging2 = service.GetProducts(2, 2).ToList();

            Assert.AreEqual(1, resultPaging2.Count());
            Assert.AreEqual("3번 상품", resultPaging2[0].ProductName);
            Assert.AreEqual(StringConst.EmptyImagePath, resultPaging2[0].ProductMainImagePath);
        }

        [TestMethod]
        public void GetProduct()
        {        
            var service = new ProductService(context);
            var result = service.GetProduct(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.ProductImages.Count());
            Assert.AreEqual(1, result.ProductImages.ElementAt(0).ImageUseTypeId);
            Assert.AreEqual("~/Images/1_T1_1.png", result.ProductImages.ElementAt(0).ImagePath);
            Assert.AreEqual("~/Images/1_T3_1.jpg", result.ProductImages.ElementAt(1).ImagePath);           
        }

    }
}