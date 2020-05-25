using System.Collections.Generic;
using Portfolio.Entities.Models;
using X.PagedList;

namespace Portfolio.Services.DTO
{
    public class InquiryContainAnswersDto
    {
        /// <summary>
        /// 질문과 답변이 포함된 리스트
        /// </summary>
        public IEnumerable<Inquiry> Inquiries { get; set; }

        /// <summary>
        /// 페이지 처리를 위해 질문만을 고려하여 계산된 PagedList (실제 질문 리스트는 Inquiries 를 사용하고, Pagenation 을 생성하기 위해 사용 )
        /// </summary>
        public IPagedList<Inquiry> Pagenation { get; set; }
    }
}
