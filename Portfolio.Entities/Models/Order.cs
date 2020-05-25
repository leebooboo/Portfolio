using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class Order
    {
        public long OrderId { get; set; }
        /// <summary>
        /// 주문번호 2020042812345
        /// </summary>
        [StringLength(13, MinimumLength = 13)]
        [Index("IX_OrderNo")]
        [Display(Name = "주문번호")]
        public string OrderNo { get; set; }

        [Index("IX_UserId_InsertDt", Order = 1)]
        [StringLength(128), Required]
        public string UserId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "주문금액")]
        public decimal TotalPrice { get; set; }

        public int OrderTypeId { get; set; }

        public OrderType OrderType { get; set; }

        [ForeignKey("OrderId")]
        public ICollection<OrderDetail> OrderDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Index("IX_UserId_InsertDt", Order = 2)]
        [Display(Name = "주문일")]
        public DateTime InsertDt { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [Display(Name = "수정일시")]
        public Nullable<DateTime> UpdateDt { get; set; }
    }
}
