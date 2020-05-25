using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Models;
using X.PagedList;
using Portfolio.Entities.Enums;

namespace Portfolio.Services.Services
{
    public class InquiryService :  IInquiryService
    {
        private PortfolioContext db;
        private ICommonService commonService;

        public InquiryService(PortfolioContext context)
        {
            db = context;
            commonService = new CommonService(db);
        }
             
        /// <summary>
        /// 질문 게시판 리스트 조회
        /// </summary>
        public InquiryContainAnswersDto GetList(int pageNumber, int pageSize)
        {
            var lv0_inquiries = db.Inquiries.Include("Product")
                                            .Where(x => x.RefLevel == 0)
                                            .OrderByDescending(x => x.InquiryId)
                                            .ToPagedList(pageNumber, pageSize);
            
            return GetListCore(lv0_inquiries);
        }

        /// <summary>
        /// 상품 뷰 페이지에서 해당 상품에 대한 질문과 답변을 조회
        /// </summary>
        public InquiryContainAnswersDto GetList(long productId, int pageNumber, int pageSize)
        {
            var lv0_inquiries = db.Inquiries.Include("Product")
                                            .Where(x => x.ProductId == productId && x.RefLevel == 0)
                                            .OrderByDescending(x => x.InquiryId)
                                            .ToPagedList(pageNumber, pageSize);
            
            return GetListCore(lv0_inquiries);
        }

        /// <summary>
        /// 질문에 대한 답변이 바인딩된 리스트를 조회
        /// </summary>
        private InquiryContainAnswersDto GetListCore(IPagedList<Inquiry> lv0_inquiries)
        {
            List<Inquiry> orderedList = new List<Inquiry>();

            InquiryContainAnswersDto dto = new InquiryContainAnswersDto();

            dto.Pagenation = lv0_inquiries;

            foreach (var Lv0 in lv0_inquiries)
            {
                orderedList.Add(Lv0);

                var mainImage = db.ProductImages.FirstOrDefault(x => x.ProductId == Lv0.ProductId && x.ImageUseTypeId == 1);

                Lv0.Product.ProductMainImagePath = mainImage != null ? mainImage.ImagePath : StringConst.EmptyImagePath;

                var inquiries = db.Inquiries.Where(x => x.InquiryId < Lv0.InquiryId && x.InquiryId > Lv0.InquiryId - 100).ToList();

                BindingAnswerList(1, Lv0.InquiryId, inquiries, ref orderedList);
            }

            dto.Inquiries = orderedList;

            return dto;
        }

        /// <summary>
        /// RefId 에 따른 하위 답변들을 바인딩 - 재귀로 동작하여 하위의 모든 답변을 바인딩
        /// </summary>
        private void BindingAnswerList(int level, long refId, IEnumerable<Inquiry> inquiries, ref List<Inquiry> orderedList)
        {
            foreach (var item in inquiries.Where(x => x.RefLevel == level && x.RefId == refId).OrderBy(x => x.InsertDt))
            {
                orderedList.Add(item);
                BindingAnswerList(item.RefLevel + 1, item.InquiryId, inquiries, ref orderedList); //재귀로 하위 답변들을 바인딩
            }
        }
               
        public void InquiryAdd(Inquiry inquiry)
        {
            try
            {
                long maxInquiryId = 0;
                if (db.Inquiries.LongCount() > 0)
                    maxInquiryId = db.Inquiries.Max(x => x.InquiryId); //항상 maxId는 100 단위 크기 ex) 19번째 질문인 경우 InquiryId = 1900

                if (inquiry.RefLevel == 0) //질문인 경우
                {
                    long addedInquiryId = maxInquiryId + 100;
                    inquiry.InquiryId = addedInquiryId;
                }
                else //답변인 경우
                {
                    var parentInquiry = db.Inquiries.FirstOrDefault(x => x.ProductId == inquiry.ProductId && x.InquiryId == inquiry.RefId);
                    if (parentInquiry == null)
                        throw new Exception("일치하는 상위 질의를 찾을 수 없습니다.");

                    long mainInquiryId = (long)Math.Ceiling(parentInquiry.InquiryId / 100m) * 100;

                    var tmpList = db.Inquiries.Where(x => x.InquiryId <= mainInquiryId && x.InquiryId > mainInquiryId - 100);
                    long minInquiryId = db.Inquiries.Where(x => x.InquiryId <= mainInquiryId && x.InquiryId > mainInquiryId - 100).Min(x => x.InquiryId);

                    inquiry.InquiryId = minInquiryId - 1;
                }

                inquiry.InsertDt = DateTime.Now;

                db.Inquiries.Add(inquiry);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
                                 
        }

        public Inquiry View(long inquiryId)
        {            
            var model = db.Inquiries.Include("Product").FirstOrDefault(x => x.InquiryId == inquiryId);

            var mainImage = db.ProductImages.FirstOrDefault(x => x.ProductId == model.ProductId && x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일);
            model.Product.ProductMainImagePath = mainImage == null ? StringConst.EmptyImagePath : mainImage.ImagePath;
            if (model != null)
                return model;
            else
                throw new Exception("일치하는 문의글을 찾을 수 없습니다.");
        }

        public void Update(Inquiry model)
        {
            Inquiry inquiry = db.Inquiries.FirstOrDefault(x => x.InquiryId == model.InquiryId);
            if (inquiry == null)
                throw new Exception("이미 삭제된 문의입니다.");

            inquiry.UpdateDt = DateTime.Now;
            inquiry.Title = model.Title;
            inquiry.Content = model.Content;
            inquiry.IsLocked = model.IsLocked;

            db.SaveChanges();
        }
        
        public List<ProductDropdown> GetProductList()
        {
            return commonService.GetProductList();
        }

        public void Delete(long inquiryId, string userId)
        {
            var inquiry = db.Inquiries.FirstOrDefault(x => x.InquiryId == inquiryId);
            if (inquiry == null)
                throw new Exception("삭제하려고하는 상품문의는 존재하지 않습니다.");

            if (inquiry.UserId != userId)
                throw new Exception("작성자만 삭제할 수 있습니다.");

            DeleteCore(inquiry);
        }

        public void DeleteAdmin(long inquiryId)
        {
            var inquiry = db.Inquiries.FirstOrDefault(x => x.InquiryId == inquiryId);
            if (inquiry == null)
                throw new Exception("삭제하려고하는 상품문의는 존재하지 않습니다.");

            DeleteCore(inquiry);
        }

        private void DeleteCore(Inquiry inquiry)
        {
            db.Entry(inquiry).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
