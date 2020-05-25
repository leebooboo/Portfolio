using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace AspNet.BoardGameMall.Utils.Extensions
{
    public static class ModelStateErrorHandler
    {
        /// <summary>
        /// ModelState의 에러들을 딕셔너리로 반환
        /// </summary>
        public static Dictionary<string, string> GetModelErrors(this ModelStateDictionary errDictionary)
        {
            var errors = new Dictionary<string, string>();
            errDictionary.Where(k => k.Value.Errors.Count > 0).ForEach(i =>
            {
                var er = string.Join(", ", i.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                errors.Add(i.Key, er);
            });
            return errors;
        }

        /// <summary>
        /// ModelState의 에러들을 단일 텍스트로 생성 ex) [키] - 에러 텍스트
        /// </summary>
        public static string StringifyModelErrors(this ModelStateDictionary errDictionary)
        {
            var errorsBuilder = new StringBuilder();
            var errors = errDictionary.GetModelErrors();
            errors.ForEach(key => errorsBuilder.AppendLine($"[{key.Key}] - {key.Value}"));
            return errorsBuilder.ToString();
        }
    }
}