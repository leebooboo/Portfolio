using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class Checkout
    {
        public long CheckoutId { get; set; }

        [StringLength(128), Required]
        [Index("IX_UserId_ProductId", Order = 1)]
        public string UserId { get; set; }

        [Index("IX_UserId_ProductId", Order = 2)]
        public long ProductId { get; set; }

        [Display(Name = "수량")]
        public int ProductCount { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "주문일")]
        public DateTime InsertDt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "수정일시")]
        public Nullable<DateTime> UpdateDt { get; set; }
    }
}
