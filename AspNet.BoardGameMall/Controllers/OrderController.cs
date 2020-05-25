using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    [Authorize()]
    public class OrderController : Controller
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        
        public ActionResult List(DateTime? startDt = null, DateTime? endDt = null)
        {
            startDt = startDt ?? DateTime.Now.AddMonths(-1);
            endDt = endDt ?? DateTime.Now;
            var model = orderService.GetOrderList(User.Identity.GetUserId(), (DateTime)startDt, (DateTime)endDt);

            ViewBag.startDt = startDt;
            ViewBag.endDt = endDt;
            return View(model);
        }

        public ActionResult View(long? id, DateTime? startDt = null, DateTime? endDt = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            startDt = startDt ?? DateTime.Now.AddMonths(-1);
            endDt = endDt ?? DateTime.Now;

            ViewBag.startDt = startDt;
            ViewBag.endDt = endDt;

            var model = orderService.GetOrder((long)id, User.Identity.GetUserId());

            return View(model);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(List<CheckoutItemDto> products)
        {            
            return View(products.Where(x => x.IsChecked).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(List<CheckoutItemDto> productList)
        {
            orderService.AddOrder(productList, User.Identity.GetUserId());

            return RedirectToAction("List");
        }

        public ActionResult DirectPurchase(long productId, int productCount, decimal price, string productName, string mainImagePath)
        {
            List<CheckoutItemDto> list = new List<CheckoutItemDto>();
            list.Add(new CheckoutItemDto {
                IsChecked = true,
                ProductId = productId,
                ProductCount = productCount,
                Price = price,
                MainImagePath = mainImagePath,
                ProductName = productName
            });

            return View("Add", list);
        }

    }
}