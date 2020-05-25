using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNet.BoardGameMall
{
    public static class CustomHtmlHepler
    {
        /// <summary>
        /// JQuery ajax Post 전송시에 위조방지키를 생성
        /// 사용방법: $.ajax({ data:{ @Html.AntiForgeryTokenForAjaxPost() });
        /// </summary>
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();
            // Above gets the following: <input name="__RequestVerificationToken" type="hidden" value="PnQE7R0MIBBAzC7SqtVvwrJpGbRvPgzWHo5dSyoSaZoabRjf9pCyzjujYBU_qKDJmwIOiPRDwBV1TNVdXFVgzAvN9_l2yt9-nf4Owif0qIDz7WRAmydVPIm6_pmJAI--wvvFQO7g0VvoFArFtAR2v6Ch1wmXCZ89v0-lNOGZLZc1" />
            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
            return new MvcHtmlString($"__RequestVerificationToken:\"{tokenValue}\"");
        }

        /// <summary>
        /// 메뉴 상단의 어떤 메뉴인지를 알려주는 부분을 생성
        /// </summary>
        public static MvcHtmlString MenuIdentity(this HtmlHelper helper, string menuText)
        {
            TagBuilder hr = new TagBuilder("hr");

            TagBuilder h4 = new TagBuilder("h4");
            h4.AddCssClass("text-center m-3");

            TagBuilder b = new TagBuilder("b");
            b.SetInnerText(menuText);

            h4.InnerHtml = b.ToString();
            
            TagBuilder hr2 = new TagBuilder("hr");
            hr2.AddCssClass("mb-4");

            string htmlString = hr.ToString() + h4.ToString() + hr2.ToString();

            return new MvcHtmlString(htmlString);
        }

    }
}