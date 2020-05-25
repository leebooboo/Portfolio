using System;
using System.Net;
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
    /// 질문게시판
    /// </summary>
    public class InquiryController : Controller
    {
        private IInquiryService inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            this.inquiryService = inquiryService;
        }
       
        public ActionResult List(int page = 1, int pageSize = 10)
        {
            if (TempData["IsAlertifySuccess"] != null)
                ViewBag.IsAlertifySuccess = TempData["IsAlertifySuccess"];

            if (TempData["AlertifySuccessMsg"] != null)
                ViewBag.AlertifySuccessMsg = TempData["AlertifySuccessMsg"];

            if (TempData["IsAlertifyError"] != null)
                ViewBag.IsAlertifyError = TempData["IsAlertifyError"];

            if (TempData["AlertifyErrorMsg"] != null)
                ViewBag.AlertifyErrorMsg = TempData["AlertifyErrorMsg"];

            return View(inquiryService.GetList(page, pageSize));
        }

        /// <summary>
        /// 상품 상세 페이지의 상품 문의 파샬뷰
        /// </summary>
        public PartialViewResult ProductViewInquiry(long productId, int page)
        {
            return PartialView(inquiryService.GetList(productId, page));
        }

        [Authorize]
        [HttpGet]
        public ActionResult InquiryAddFromProductView(long productId, string productName, string productImage = null, int refLevel = 0, Nullable<long> refId = null)
        {
            Inquiry model = new Inquiry()
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
        [ValidateInput(false)] //inquiry.Content 에 html 태그들이 들어가서 ValidateInput(false)를 해주지 않으면 액션에서 에러처리가 됨
        public JsonResult InquiryAddFromProductView(Inquiry inquiry)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return Json(new ResultJson(false, null, ModelState.StringifyModelErrors()));
                }

                inquiry.IpAdress = Util.GetIpAddress(Request);
                inquiry.UserId = User.Identity.GetUserId();
                inquiry.UserName = Util.GetProtectedUserName(User);
                inquiry.InsertDt = DateTime.Now;

                inquiryService.InquiryAdd(inquiry);

                return Json(new ResultJson(true));
            }
            catch(Exception ex)
            {
                return Json(new ResultJson(false, null, ex.Message));
            }            
        }

        public ActionResult View(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = inquiryService.View((long)id);

            if(model.IsLocked)
            {
                if (!User.IsInRole("Admin"))
                {
                    if(User.Identity.GetUserId() != model.UserId)
                    {
                        TempData["IsAlertifyError"] = true;
                        TempData["AlertifyErrorMsg"] = "비공개 문의글에 대한 접근 권한이 없습니다.";

                        var dic = Util.GetPageAndPageSize_FromPreviewPage(Request);
                        return RedirectToAction("List", new { page = dic["Page"], pageSize = dic["PageSize"] });
                    }
                }
            }

            var kv = Util.GetPageAndPageSize_FromPreviewPage(Request);
            ViewBag.page = kv["Page"];
            ViewBag.pageSize = kv["PageSize"];

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = inquiryService.View((long)id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Inquiry model)
        {
            if (!ModelState.IsValid)
            {
                return View(inquiryService.View(model.InquiryId));
            }

            try
            {
                inquiryService.Update(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }

            return RedirectToAction("View", new { id = model.InquiryId });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            Inquiry model = new Inquiry
            {
                RefLevel = 0
            };

            var productList = inquiryService.GetProductList();
            ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName", "CategoryName", 0);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Inquiry model)
        {
            model.InsertDt = DateTime.Now;
            model.IpAdress = Util.GetIpAddress(Request);
            model.UserId = User.Identity.GetUserId();
            model.UserName = Util.GetProtectedUserName(User);

            if (!ModelState.IsValid)
            {
                var productList = inquiryService.GetProductList();
                ViewBag.ProductId = new SelectList(productList, "ProductId", "ProductName", "CategoryName", 0);
                return View(model);
            }

            inquiryService.InquiryAdd(model);

            return RedirectToAction("List");
        }

        [Authorize]
        public ActionResult Reply(long productId, int refLevel, long refId, string refTitle, bool isLocked)
        {
            Inquiry model = new Inquiry
            {
                ProductId = productId,
                RefLevel = refLevel,
                RefId = refId,
                Title = $"Re:{refTitle}",
                IsLocked = isLocked
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Reply(Inquiry model)
        {
            model.InsertDt = DateTime.Now;
            model.IpAdress = Util.GetIpAddress(Request);
            model.UserId = User.Identity.GetUserId();
            model.UserName = Util.GetProtectedUserName(User);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            inquiryService.InquiryAdd(model);

            return RedirectToAction("List", new { page = ViewBag.page ?? 1, pageSize = ViewBag.pageSize ?? 10 });
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult Delete(long inquiryId)
        {
            if (User.IsInRole("Admin"))
            {
                inquiryService.DeleteAdmin(inquiryId);
            }
            else
            {
                inquiryService.Delete(inquiryId, User.Identity.GetUserId());
            }

            TempData["IsAlertifySuccess"] = true;
            TempData["AlertifySuccessMsg"] = "삭제 요청이 완료됐습니다.";

            return RedirectToAction("List");
        }

    }
}