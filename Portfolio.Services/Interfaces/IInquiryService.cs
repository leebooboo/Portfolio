using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Entities.Models;
using X.PagedList;

namespace Portfolio.Services.Interfaces
{
    public interface IInquiryService
    {
        /// <summary>
        /// 질문 게시판 리스트 조회
        /// </summary>
        InquiryContainAnswersDto GetList(int pageNumber = 1, int pageSize = 5);

        /// <summary>
        /// 상품 뷰 페이지에서 해당 상품에 대한 질문과 답변을 조회
        /// </summary>
        InquiryContainAnswersDto GetList(long productId, int pageNumber = 1, int pageSize = 5);

        void InquiryAdd(Inquiry inquiry);

        Inquiry View(long inquiryId);

        void Update(Inquiry review);

        List<ProductDropdown> GetProductList();

        void Delete(long inquiryId, string userId);

        void DeleteAdmin(long inquiryId);

    }
}
