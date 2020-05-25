using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNet.BoardGameMall.Models
{
    public class ProductListViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Summary { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public Nullable<decimal> PromotionPrice { get; set; }
        public string ImagePath { get; set; }
    }
}