using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;

namespace AspNet.BoardGameMall.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> ProductList { get; set; }
        public int Page { get; set; }
    }
}