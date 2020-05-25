using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class Inquiry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long InquiryId { get; set; }

        [Index("IX_ProductId")]
        public long ProductId { get; set; }

        public Nullable<long> RefId { get; set; }
        
        public int RefLevel { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "이름")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "제목은 50자를 초과할 수 없습니다.")]
        [MinLength(2, ErrorMessage = "제목을 2자이상 입력해주세요.")]
        [Display(Name = "제목")]
        public string Title { get; set; }

        [StringLength(2400), Required]
        [Display(Name = "문의 내용")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "비공개")]
        public bool IsLocked { get; set; }

        [Display(Name = "IP 주소")]
        public string IpAdress { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "작성일")]
        public DateTime InsertDt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "수정일시")]
        public Nullable<DateTime> UpdateDt { get; set; }

        public Product Product { get; set; }
    }
}
