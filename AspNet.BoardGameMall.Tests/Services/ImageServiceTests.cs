using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;
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
    public class ImageServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var imageUseTypes = new List<ImageUseType>
            {
                new ImageUseType{ ImageUseTypeId = 1, TypeName = "메인 섬네일", UseYn = true },
                new ImageUseType{ ImageUseTypeId = 2, TypeName = "서브 섬네일", UseYn = true },
                new ImageUseType{ ImageUseTypeId = 3, TypeName = "상품 세부 이미지", UseYn = true },
                new ImageUseType{ ImageUseTypeId = 4, TypeName = "상품 문의 이미지", UseYn = true },
                new ImageUseType{ ImageUseTypeId = 5, TypeName = "상품 후기 이미지", UseYn = true }
            };

            context.ImageUseTypes.AddRange(imageUseTypes);
            context.SaveChanges();
        }

        [TestMethod]
        public void InsertProductImages()
        {
            ProductImagesDto dto_Main = new ProductImagesDto
            {
                ProductId = 1,
                ImageUseTypeId = (int)ImageUseTypeEnum.메인섬네일,
                ServerPath = StringConst.ProductImageUploadPath,
                ProductImageList = new List<ProductImageDto>
                {
                    new ProductImageDto
                    {
                        ImageSize = 1024,
                        ImageName = "1 메인.png",
                        ImageType = "png"
                    }
                }
            };

            ProductImagesDto dto_Detail = new ProductImagesDto
            {
                ProductId = 1,
                ImageUseTypeId = (int)ImageUseTypeEnum.상품세부이미지,
                ServerPath = StringConst.ProductImageUploadPath,
                ProductImageList = new List<ProductImageDto>
                {
                    new ProductImageDto
                    {
                        ImageSize = 1024,
                        ImageName = "1 상세1.png",
                        ImageType = "png"
                    },
                    new ProductImageDto
                    {
                        ImageSize = 2048,
                        ImageName = "1 상세2.jpg",
                        ImageType = "jpg"
                    }
                }
            };

            var service = new ImageService(context);
            service.InsertProductImages(dto_Main);
            service.InsertProductImages(dto_Detail);

            var results_Main = context.ProductImages.Where(x => x.ProductId == 1 && x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일).ToList();
            Assert.AreEqual(1, results_Main.Count);
            Assert.AreEqual("1 메인.png", results_Main[0].ImageName);
            Assert.AreEqual($"{StringConst.ProductImageUploadPath}/1_T1_1.{results_Main[0].ImageType}", results_Main[0].ImagePath);

            var results_Detail = context.ProductImages.Where(x => x.ProductId == 1 && x.ImageUseTypeId == (int)ImageUseTypeEnum.상품세부이미지).ToList();
            Assert.AreEqual(2, results_Detail.Count);
            Assert.AreEqual("1 상세1.png", results_Detail[0].ImageName);
            Assert.AreEqual("1 상세2.jpg", results_Detail[1].ImageName);
            Assert.AreEqual($"{StringConst.ProductImageUploadPath}/1_T3_1.{results_Detail[0].ImageType}", results_Detail[0].ImagePath);
            Assert.AreEqual($"{StringConst.ProductImageUploadPath}/1_T3_2.{results_Detail[1].ImageType}", results_Detail[1].ImagePath);
        }

        [TestMethod]
        public void InsertUploadImage()
        {
            var dto = new UploadImageDto
            {
                ServerPath = StringConst.UploadImageUploadPath,
                ProductId = 1,
                ImageUseTypeId = (int)ImageUseTypeEnum.상품문의이미지,
                ImageName = "상품문의1.png",
                ImageSize = 1024,
                ImageType = "png"
            };

            var service = new ImageService(context);
            service.InsertUploadImage(dto);

            var result = context.UploadImages.Where(x => x.ProductId == 1 && x.ImageUseTypeId == (int)ImageUseTypeEnum.상품문의이미지).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("상품문의1.png", result[0].ImageName);
            Assert.AreEqual($"{StringConst.UploadImageUploadPath}/1_T4_1.{result[0].ImageType}", result[0].ImagePath);
        }
    }
}
