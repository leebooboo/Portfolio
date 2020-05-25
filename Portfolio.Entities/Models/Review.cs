using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ReviewId { get; set; }

        [Index("IX_ProductId")]
        public long ProductId { get; set; }

        public Nullable<long> RefId { get; set; }

        public int RefLevel { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "이름")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "제목을 입력해주세요.")]
        [MaxLength(50, ErrorMessage = "제목은 50자를 초과할 수 없습니다.")]
        [MinLength(2, ErrorMessage = "제목을 2자이상 입력해주세요.")]
        [Display(Name = "제목")]
        public string Title { get; set; }

        [Required(ErrorMessage = "상품 후기를 입력해주세요.")]
        [StringLength(2400)]
        [Display(Name = "상품 후기")]
        public string Content { get; set; }

        [Display(Name = "IP 주소")]
        public string IpAdress { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "작성일")]
        public DateTime InsertDt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "수정일시")]
        public Nullable<DateTime> UpdateDt { get; set; }

        /// <summary>
        /// 별점
        /// </summary>
        [Range(0, 5, ErrorMessage = "0 ~ 5점 사이로 별점을 평가해주세요.")]
        [Display(Name = "평점")]
        [DisplayFormat(DataFormatString = "{0:0.#}")]
        public Nullable<decimal> Rating { get; set; }

        public Product Product { get; set; }
    }
}
