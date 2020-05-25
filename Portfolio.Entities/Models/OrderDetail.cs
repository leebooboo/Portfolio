using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entities.Models
{
    public class OrderDetail
    {
        public long OrderDetailId { get; set; }

        [Index("IX_OrderId")]
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "판매가")]
        public decimal Price { get; set; }

        [Display(Name = "수량")]
        public int Count { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        [Display(Name = "상품금액")]
        public decimal SumPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "주문일")]
        public DateTime InsertDt { get; set; }
    }
}
