using System;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using AspNet.BoardGameMall.Models;
using AspNet.BoardGameMall.Utils;
using AspNet.BoardGameMall.Utils.Extensions;
using Microsoft.AspNet.Identity;
using Portfolio.Entities.Models;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    /// <summary>
    /// 상품 후기 게시판
    /// </summary>
    public class ReviewController : Controller
    {
        private IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public ActionResult List(int page = 1, int pageSize = 10)
        {
            return View(reviewService.GetList(page, pageSize));
        }

        /// <summary>
        /// 상품 상세 페이지의 상품 후기 파샬뷰
        /// </summary>
        public PartialViewResult ProductViewReview(long productId, int page)
        {
            return PartialView(reviewService.GetList(productId, page));
        }

        [Authorize]
        [HttpGet]
        public ActionResult ReviewAddFromProductView(long productId, string productName, string productImage = null, int refLevel = 0, Nullable<long> refId = null)
        {
            Review model = new Review()
            {
                ProductId = productId,
                RefLevel = refLevel,
                RefId = refId
            };
            ViewBag.productName = productName;
            ViewBag.productImage = productImage;
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)] //reivew.Content 에 html 태그들이 들어가서 ValidateInput(false)를 해주지 않으면 액션에서 에러처리가 됨
        public ActionResult ReviewAddFromProductView(Review review)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new ResultJson(false, null, ModelState.StringifyModelErrors()));
                }

                var ci = User.Identity as ClaimsIdentity;

                review.IpAdress = Util.GetIpAddress(Request);
                review.UserId = User.Identity.GetUserId();
                review.UserName = Util.GetProtectedUserName(User);
                review.InsertDt = DateTime.Now;

                reviewService.ReviewAdd(review);

                return Json(new ResultJson(true));
            }
            catch (Exception ex)
            {
                return Json(new ResultJson(false, null, ex.Message));
            }
        }

        public ActionResult View(long? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var model = reviewService.View((long)id);
            if(Request.UrlReferrer != null && Request.UrlReferrer.Query.Length >= 1)
            {
                string queryString = Request.UrlReferrer.Query;
                string[] queryStrings = queryString.Substring(1).Split('&');
                foreach(string query in queryStrings)
                {
                    if(query.ToLower().Contains("page="))
                    {
                        ViewBag.page = query.Split('=')[1];
                        continue;
                    }

                    if (query.ToLower().Contains("pagesize="))
                    {
                        ViewBag.pageSize = query.Split('=')[1];
                        continue;
                    }
                }
            }

            return View(model);
        }
                
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = reviewService.View((long)id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Review model)
        {            
            if (!ModelState.IsValid)
            {
                return View(reviewService.View(model.ReviewId));
            }

            try
            {
                reviewService.Update(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }

            return RedirectToAction("View", new { id = model.ReviewId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            Review model = new Review
            {
                Rating = 5,
                RefLevel = 0
            };
            
            var productList = reviewService.GetProductList();
            
            ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName", "CategoryName", 0);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Review model)
        {
            model.InsertDt = DateTime.Now;
            model.IpAdress = Util.GetIpAddress(Request);
            model.UserId = User.Identity.GetUserId();
            model.UserName = Util.GetProtectedUserName(User);

            if(!ModelState.IsValid)
            {
                var productList = reviewService.GetProductList();
                ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName", "CategoryName", 0);
                return View(model);                
            }

            reviewService.ReviewAdd(model);

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult Reply(long productId, int refLevel, long refId, string refTitle)
        {
            Review model = new Review
            {
                ProductId = productId,
                RefLevel = refLevel,
                RefId = refId,
                Title = $"Re:{refTitle}"
            };
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Reply(Review model)
        {
            model.InsertDt = DateTime.Now;
            model.IpAdress = Util.GetIpAddress(Request);
            model.UserId = User.Identity.GetUserId();
            model.UserName = Util.GetProtectedUserName(User);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            reviewService.ReviewAdd(model);

            return RedirectToAction("List", new { page = ViewBag.page ?? 1, pageSize = ViewBag.pageSize ?? 10});
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Delete(long reviewId)
        {            
            if (User.IsInRole("Admin"))
            {
                reviewService.DeleteAdmin(reviewId);
            }
            else
            {
                reviewService.Delete(reviewId, User.Identity.GetUserId());
            }

            TempData["IsAlertifySuccess"] = true;
            TempData["AlertifySuccessMsg"] = "삭제 요청이 완료됐습니다.";
            
            return RedirectToAction("List");
        }
    }
}