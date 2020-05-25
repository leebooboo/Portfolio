using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities.Models
{
    public class ImageUseType
    {
        [Key]
        public int ImageUseTypeId { get; set; }

        [StringLength(20)]
        [Display(Name = "구분")]
        public string TypeName { get; set; }

        public bool UseYn { get; set; }
    }
}
