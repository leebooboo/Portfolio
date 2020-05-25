using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet.BoardGameMall.Models
{
    public class ResultJson
    {
        public ResultJson(bool isSuccess, string successMessage = null, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            SuccessMessage = successMessage;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}