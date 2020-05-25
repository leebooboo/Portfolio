using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Enums;

namespace Portfolio.Services.Services
{
    public class ImageService : IImageService
    {
        private PortfolioContext db;

        public ImageService(PortfolioContext context)
        {
            db = context;
        }

        /// <summary>
        /// ProductImageDTO 리스트를 전달받아 ProductImages 테이블에 데이터 Insert
        /// ProductImage 는 메인 섬네일, 서브 섬네일, 상품 세부 이미지 등의 상품 판매 관련 이미지를 관리
        /// </summary>
        public void InsertProductImages(ProductImagesDto dto)
        {
            long productId = dto.ProductId;
            int imageUseTypeId = dto.ImageUseTypeId;

            var existImages = db.ProductImages.AsNoTracking().Where(x => x.ProductId == productId && x.ImageUseTypeId == imageUseTypeId).ToList();
            
            int seq = existImages.Count == 0 ? 0 : Int32.Parse(existImages.Max(x => x.ImagePath).Split('_').Last().Split('.').First());

            foreach (var item in dto.ProductImageList)
            {
                seq++;

                item.ImagePath = $"{dto.ServerPath}/{dto.ProductId}_T{dto.ImageUseTypeId}_{seq}.{item.ImageType}";

                db.ProductImages.Add(new Entities.Models.ProductImage
                {
                    ProductId = dto.ProductId,
                    ImageUseTypeId = dto.ImageUseTypeId,
                    ImageName = item.ImageName,
                    ImageType = item.ImageType,
                    ImageSize = item.ImageSize,
                    ImagePath = item.ImagePath,
                    InsertDt = DateTime.Now
                });
            }

            db.SaveChanges();
        }

        /// <summary>
        /// UploadImageDTO 리스트를 전달받아 UploadImages 테이블에 데이터 Insert
        /// UploadImage 는 상품 문의, 상품 리뷰 등의 이미지를 관리
        /// </summary>
        public void InsertUploadImage(UploadImageDto dto)
        {
            long productId = dto.ProductId;
            int imageUseTypeId = dto.ImageUseTypeId;

            var existImages = db.UploadImages.AsNoTracking().Where(x => x.ProductId == productId && x.ImageUseTypeId == imageUseTypeId).OrderBy(x => x.ImagePath).ToList();

            int lastSeq = existImages.Count == 0 ? 0 : Int32.Parse(existImages.Last().ImagePath.Split('_').Last().Split('.').First());
                
            dto.ImagePath = $"{dto.ServerPath}/{dto.ProductId}_T{dto.ImageUseTypeId}_{lastSeq + 1}.{dto.ImageType}";

            db.UploadImages.Add(new Entities.Models.UploadImage
            {
                ProductId = dto.ProductId,
                ImageUseTypeId = dto.ImageUseTypeId,
                ImageName = dto.ImageName,
                ImageType = dto.ImageType,
                ImageSize = dto.ImageSize,
                ImagePath = dto.ImagePath,
                InsertDt = DateTime.Now
            });
            
            db.SaveChanges();
        }
    }
}
