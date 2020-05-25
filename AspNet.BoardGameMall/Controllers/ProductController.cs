using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private ICategoryService categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public ActionResult Search(string name)
        {            
            ViewBag.Title = $"{name} 검색";
            
            var model = productService.GetProducts(name);

            ViewBag.SearchText = name;
            ViewBag.SearchCount = model.Count();

            return View(model);
        }

        public ActionResult Category(int id)
        {
            int categoryId = id;

            ViewBag.CategoryName = categoryService.GetCategory(categoryId);

            var model = productService.GetCategoryProducts(categoryId);
            
            return View(model);
        }

        public ActionResult ProductList(int page, int count = 20)
        {
            var model = productService.GetProducts(page, count);
            return PartialView("ProductList", model);
        }

        public ActionResult View(long id)
        {
            ViewBag.userId = HttpContext.User.Identity.GetUserId();

            var model = productService.GetProduct(id);
            
            return View(model);
        }

        /// <summary>
        /// 상품 페이지의 상품상세정보/상품후기/상품문의/배송정보/교환및반품정보 탭
        /// </summary>
        public PartialViewResult ProductViewTab()
        {
            return PartialView();
        }

        /// <summary>
        /// 상품 페이지의 배송정보
        /// </summary>
        public PartialViewResult DeliveryInfo()
        {
            return PartialView();
        }

        /// <summary>
        /// 상품 페이지의 교환 및 반품정보
        /// </summary>
        public PartialViewResult ExchangeInfo()
        {
            return PartialView();
        }

    }
}