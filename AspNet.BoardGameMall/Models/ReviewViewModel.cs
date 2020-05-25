using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.BoardGameMall.Models
{
    public class ReviewViewModel
    {
        public long ReviewId { get; set; }
        public long ProductId { get; set; }
        public Nullable<long> RefId { get; set; }
        public int RefLevel { get; set; }

    }
}