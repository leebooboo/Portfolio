using System.Web;
using System.Web.Optimization;

namespace AspNet.BoardGameMall
{
    public class BundleConfig
    {
        // 묶음에 대한 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=301862를 참조하세요.
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Style (css)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Libraries/alertifyjs/css").Include(
                      "~/Libraries/alertifyjs/css/alertify.css",
                      "~/Libraries/alertifyjs/css/themes/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Libraries/star-rating-svg/css").Include(
                          "~/Libraries/star-rating-svg/css/star-rating-svg.css"));
            #endregion


            #region Script (js)
            // Modernizr의 개발 버전을 사용하여 개발하고 배우십시오. 그런 다음
            // 프로덕션에 사용할 준비를 하고 https://modernizr.com의 빌드 도구를 사용하여 필요한 테스트만 선택하세요.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/jquery.unobtrusive-ajax.js",
                            "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new ScriptBundle("~/Libraries/alertifyjs").Include(
                      "~/Libraries/alertifyjs/alertify.js",
                      "~/Scripts/alertify.init.setting.js"));

            bundles.Add(new ScriptBundle("~/Libraries/ckeditor").Include(
                      "~/Libraries/ckeditor/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/Libraries/star-rating-svg").Include(
                      "~/Libraries/star-rating-svg/jquery.star-rating-svg.js"));
            #endregion



        }
    }
}
