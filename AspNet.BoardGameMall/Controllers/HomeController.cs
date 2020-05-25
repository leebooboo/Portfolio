using System.Web.Mvc;
using AspNet.BoardGameMall.Models;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            var productList = productService.GetProducts(1, 20);

            var model = new HomeIndexViewModel { ProductList = productList, Page = 2 };

            return View(model);
        }
                
    }
}