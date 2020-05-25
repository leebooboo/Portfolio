using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;

namespace Portfolio.Services.Interfaces
{
    public interface IReviewService
    {
        /// <summary>
        /// 상품 후기 게시판 리스트 조회
        /// </summary>
        ReviewContainAnswersDto GetList(int pageNumber = 1, int pageSize = 5);

        /// <summary>
        /// 상품 뷰 페이지에서 해당 상품에 대한 상품 후기와 답변을 조회
        /// </summary>
        ReviewContainAnswersDto GetList(long productId, int pageNumber = 1, int pageSize = 5);

        void ReviewAdd(Review review);

        Review View(long reviewId);

        void Update(Review review);
        
        List<ProductDropdown> GetProductList();

        void Delete(long reviewId, string userId);

        void DeleteAdmin(long reviewId);
    }
}
