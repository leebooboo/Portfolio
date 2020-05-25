using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.BoardGameMall.Models
{
    public class UploadImageResponse
    {
        public int uploaded { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }
        public UploadImageError error { get; set; }
    }

    public class UploadImageError
    {
        public UploadImageError(string message)
        {
            this.message = message;
        }

        public string message { get; set; }
    }
}