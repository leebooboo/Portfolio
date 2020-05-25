using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(20)]
        [Display(Name = "카테고리 코드")]
        public string CategoryCode { get; set; }     

        [StringLength(20), Required]
        [Display(Name = "카테고리명")]
        public string CategoryName { get; set; }

        [StringLength(50)]
        [Display(Name = "설명")]
        public string Description { get; set; }

        public int Order { get; set; }
    }
}
