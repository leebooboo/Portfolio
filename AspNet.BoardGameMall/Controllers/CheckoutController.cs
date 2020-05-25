using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet.BoardGameMall.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    public class CheckoutController : Controller
    {
        private ICheckoutService checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            this.checkoutService = checkoutService;
        }

        // GET: Checkout
        public ActionResult Index()
        {
            IEnumerable<CheckoutItemDto> list;
                        
            if (User.Identity.IsAuthenticated) //로그인 상태
            {
                list = checkoutService.GetList(User.Identity.GetUserId());                
            }
            else //로그인 안된 상태
            {
                if(HttpContext.Request.Cookies.AllKeys.Contains("Checkout"))
                {
                    var checkoutCookie = HttpUtility.UrlDecode(HttpContext.Request.Cookies["Checkout"].Value.ToString());
                    IEnumerable<Checkout> checkoutCookieList = JsonConvert.DeserializeObject<IEnumerable<Checkout>>(checkoutCookie);

                    list = checkoutService.GetList(checkoutCookieList);
                }                
                else
                {
                    list = new List<CheckoutItemDto>();
                }                
            }

            return View(list);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public JsonResult AddCart(long productId, int productCount)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Json(new ResultJson(false, null,"인증된 사용자가 아닙니다.\r\n로그인이 필요합니다."), JsonRequestBehavior.AllowGet);
            }

            try
            {
                checkoutService.UpsertItem(User.Identity.GetUserId(), productId, productCount);                
            }
            catch (Exception ex)
            {
                return Json(new ResultJson(false, null, ex.Message));
            }

            return Json(new ResultJson(true, null, null));
        }

        /// <summary>
        /// 장바구니 Index에 로그인 상태로 진입 & 쿠키에 저장된 장바구니 상품이 있는 경우 이 액션을 실행.
        /// 쿠키에 저장된 상품들을 DB 장바구니 테이블에 추가하고 쿠키에 담긴 장바구니 리스트를 삭제함. 
        /// </summary>
        public JsonResult CookieItemsAddToDb()
        {
            try
            {
                var checkoutCookie = HttpUtility.UrlDecode(HttpContext.Request.Cookies["Checkout"].Value.ToString());
                List<Checkout> checkoutCookieList = JsonConvert.DeserializeObject<IEnumerable<Checkout>>(checkoutCookie).ToList();

                var checkoutDbList = checkoutService.GetList(User.Identity.GetUserId());

                for (int i = checkoutCookieList.Count() - 1; i >= 0; i--)
                {
                    if (checkoutDbList.FirstOrDefault(x => x.ProductId == checkoutCookieList[i].ProductId) != null)
                    {
                        checkoutCookieList.Remove(checkoutCookieList[i]); //쿠키에서 DB에 존재하는 상품을 삭제
                    }
                }

                int insertedCount = checkoutService.InsertItems(User.Identity.GetUserId(), checkoutCookieList);

                //쿠키에 담긴 장바구니 리스트 삭제
                Response.Cookies["Checkout"].Expires = DateTime.Today.AddDays(-1);

                return Json(new ResultJson(true, $"로그인 이전에 보관중이던 {insertedCount}개의 상품이 장바구니에 추가됐습니다.\n(이미 담겨있는 상품은 제외됐습니다.)"), JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new ResultJson(false, null, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //[ValidateAntiForgeryToken]
        //public ActionResult Test()
        //{
            
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteItem()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new ResultJson(false, null, "인증된 사용자가 아닙니다.\r\n로그인이 필요합니다."));
            }

            try
            {
                string[] productIds = Request.Form["productIdList[]"].Split(',');
                List<long> productIdList = new List<long>();
                foreach (string item in productIds)
                {
                    productIdList.Add(long.Parse(item));
                }

                checkoutService.DeleteItems(User.Identity.GetUserId(), productIdList);
            }
            catch (Exception ex)
            {
                return Json(new ResultJson(false, null, ex.Message));
            }

            return Json(new ResultJson(true, null, null));
        }
        

        //주문시
        // 1. 로그인 안된 경우 로그인
        // 2. 로그인 완료후 
        // 3-1. DB에 장바구니 리스트가 없는 경우, 바로 주문으로 이동
        // 3-2. DB에 장바구니 리스트가 있는 경우, Index 로 이동

        //주문시2
        // 1. 로그인 상태인 경우
        // 2. 선택된 상품 주문
    }
}