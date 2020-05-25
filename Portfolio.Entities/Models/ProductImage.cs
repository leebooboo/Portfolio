using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class ProductImage
    {
        [Key]
        public long ProductImageId { get; set; }

        [Index("IX_ProductId_ImageUseTypeId", Order = 1)]
        public long ProductId { get; set; }

        [Index("IX_ProductId_ImageUseTypeId", Order = 2)]
        public int ImageUseTypeId { get; set; }

        [StringLength(50, ErrorMessage = "이미지 파일명은 50자를 넘길 수 없습니다."), Required]
        [Display(Name = "파일명")]
        public string ImageName { get; set; }

        [StringLength(10), Required]
        [Display(Name = "구분")]
        public string ImageType { get; set; }

        [Display(Name = "파일크기")]
        public int ImageSize { get; set; }

        [StringLength(255), Required]
        [Display(Name = "이미지 경로")]
        public string ImagePath { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "작성일")]
        public DateTime InsertDt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "수정일시")]
        public Nullable<DateTime> UpdateDt { get; set; }

        public ImageUseType ImageUseType { get; set; }
    }
}
