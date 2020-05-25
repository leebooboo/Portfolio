using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;

namespace Portfolio.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// ProductImageDTO 리스트를 전달받아 ProductImages 테이블에 데이터 Insert
        /// ProductImage 는 메인 섬네일, 서브 섬네일, 상품 세부 이미지 등의 상품 판매 관련 이미지를 관리
        /// </summary>
        void InsertProductImages(ProductImagesDto dtos);

        /// <summary>
        /// UploadImageDTO 리스트를 전달받아 UploadImages 테이블에 데이터 Insert
        /// UploadImage 는 상품 문의, 상품 리뷰 등의 이미지를 관리
        /// </summary>
        void InsertUploadImage(UploadImageDto dto);
    }
}
