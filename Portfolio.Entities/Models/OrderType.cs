using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entities.Models
{
    public class OrderType
    {
        public int OrderTypeId { get; set; }

        [StringLength(20)]
        [Display(Name = "주문상태")]
        public string Type { get; set; }
    }
}
