using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;
using X.PagedList;

namespace Portfolio.Services.Services
{
    public class ReviewService : IReviewService
    {
        private PortfolioContext db;
        private ICommonService commonService;

        public ReviewService(PortfolioContext context)
        {
            db = context;
            commonService = new CommonService(db);
        }

        /// <summary>
        /// 상품후기 게시판 리스트 조회
        /// </summary>
        public ReviewContainAnswersDto GetList(int pageNumber, int pageSize)
        {
            var lv0_reviews = db.Reviews.Include("Product")
                                        .Where(x => x.RefLevel == 0)
                                        .OrderByDescending(x => x.ReviewId)                                            
                                        .ToPagedList(pageNumber, pageSize);

            return GetListCore(lv0_reviews);
        }

        /// <summary>
        /// 상품 뷰 페이지에서 해당 상품에 대한 후기와 답변을 조회
        /// </summary>
        public ReviewContainAnswersDto GetList(long productId, int pageNumber, int pageSize)
        {            
            var lv0_reviews = db.Reviews.Include("Product")
                                        .Where(x => x.ProductId == productId && x.RefLevel == 0)
                                        .OrderByDescending(x => x.ReviewId)
                                        .ToPagedList(pageNumber, pageSize);

            return GetListCore(lv0_reviews);
        }

        /// <summary>
        /// 상품후기에 대한 답변이 바인딩된 리스트를 조회
        /// </summary>
        private ReviewContainAnswersDto GetListCore(IPagedList<Review> lv0_reviews)
        {
            List<Review> orderedList = new List<Review>();

            ReviewContainAnswersDto dto = new ReviewContainAnswersDto();

            dto.Pagenation = lv0_reviews;

            foreach (var Lv0 in lv0_reviews)
            {
                orderedList.Add(Lv0);

                var mainImage = db.ProductImages.FirstOrDefault(x => x.ProductId == Lv0.ProductId && x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일);

                Lv0.Product.ProductMainImagePath = mainImage != null ? mainImage.ImagePath : StringConst.EmptyImagePath;

                var reviews = db.Reviews.Where(x => x.ReviewId < Lv0.ReviewId && x.ReviewId > Lv0.ReviewId - 100).ToList();

                BindingAnswerList(1, Lv0.ReviewId, reviews, ref orderedList);
            }

            dto.Reviews = orderedList;

            return dto;
        }

        /// <summary>
        /// RefId 에 따른 하위 답변들을 바인딩 - 재귀로 동작하여 하위의 모든 답변을 바인딩
        /// </summary>
        private void BindingAnswerList(int level, long refId, IEnumerable<Review> reviews, ref List<Review> orderedList)
        {
            foreach (var item in reviews.Where(x => x.RefLevel == level && x.RefId == refId).OrderBy(x => x.InsertDt))
            {
                orderedList.Add(item);
                BindingAnswerList(item.RefLevel + 1, item.ReviewId, reviews, ref orderedList); //재귀로 하위 답변들을 바인딩
            }
        }

        public void ReviewAdd(Review review)
        {
            try
            {
                long maxReviewId = 0;
                if (db.Reviews.LongCount() > 0)
                    maxReviewId = db.Reviews.Max(x => x.ReviewId); //항상 maxId는 100 단위 크기 ex) 19번째 질문인 경우 ReviewId = 1900

                if (review.RefLevel == 0) //질문인 경우
                {
                    long addedReviewyId = maxReviewId + 100;
                    review.ReviewId = addedReviewyId;
                }
                else //답변인 경우
                {
                    var parentReview = db.Reviews.FirstOrDefault(x => x.ProductId == review.ProductId && x.ReviewId == review.RefId);
                    if (parentReview == null)
                        throw new Exception("일치하는 상위 질의를 찾을 수 없습니다.");

                    long mainReviewId = (long)Math.Ceiling(parentReview.ReviewId / 100m) * 100;

                    var tmpList = db.Reviews.Where(x => x.ReviewId <= mainReviewId && x.ReviewId > mainReviewId - 100);
                    long minReviewId = db.Reviews.Where(x => x.ReviewId <= mainReviewId && x.ReviewId > mainReviewId - 100).Min(x => x.ReviewId);

                    review.ReviewId = minReviewId - 1;
                }

                review.InsertDt = DateTime.Now;

                db.Reviews.Add(review);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Review View(long reviewId)
        {
            var model = db.Reviews.Include("Product").FirstOrDefault(x => x.ReviewId == reviewId);

            var mainImage = db.ProductImages.FirstOrDefault(x => x.ProductId == model.ProductId && x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일);
            model.Product.ProductMainImagePath = mainImage == null ? StringConst.EmptyImagePath : mainImage.ImagePath;
            if (model != null)
                return model;
            else
                throw new Exception("일치하는 상품후기를 찾을 수 없습니다.");
        }

        public void Update(Review model)
        {
            Review review = db.Reviews.FirstOrDefault(x => x.ReviewId == model.ReviewId);
            if (review == null)
                throw new Exception("이미 삭제된 리뷰입니다.");

            review.UpdateDt = DateTime.Now;
            review.Rating = model.Rating;
            review.Title = model.Title;
            review.Content = model.Content;

            db.SaveChanges();
        }
                
        public List<ProductDropdown> GetProductList()
        {
            return commonService.GetProductList();
        }

        public void Delete(long reviewId, string userId)
        {
            var review = db.Reviews.FirstOrDefault(x => x.ReviewId == reviewId);
            if (review == null)
                throw new Exception("삭제하려고하는 리뷰는 존재하지 않는 리뷰입니다.");

            if(review.UserId != userId)
                throw new Exception("작성자만 리뷰를 삭제할 수 있습니다.");

            DeleteCore(review);
        }

        public void DeleteAdmin(long reviewId)
        {
            var review = db.Reviews.FirstOrDefault(x => x.ReviewId == reviewId);
            if (review == null)
                throw new Exception("삭제하려고하는 리뷰는 존재하지 않는 리뷰입니다.");

            DeleteCore(review);
        }

        private void DeleteCore(Review review)
        {
            db.Entry(review).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
    }
}
