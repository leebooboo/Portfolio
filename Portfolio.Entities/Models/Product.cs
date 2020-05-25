using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        public Nullable<int> CategoryId { get; set; }
        
        /// <summary>
        /// 보드게임명
        /// </summary>
        [StringLength(50), Required]
        [Index]
        [Display(Name = "상품명")]
        public string ProductName { get; set; }

        /// <summary>
        /// 간단한 보드게임 요약 설명
        /// </summary>
        [StringLength(50)]
        [Display(Name = "요약")]
        public string Summary { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "가격")]
        public decimal Price { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "할인가격")]
        public Nullable<decimal> PromotionPrice { get; set; }

        /// <summary>
        /// 상품 디테일 html
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [NotMapped]
        public string ProductMainImagePath { get; set; }
    }
}
