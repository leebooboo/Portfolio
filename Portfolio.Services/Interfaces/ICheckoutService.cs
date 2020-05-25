using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Entities.Models;

namespace Portfolio.Services.Interfaces
{
    public interface ICheckoutService
    {
        /// <summary>
        /// DB 장바구니에 담긴 상품들을 조회
        /// </summary>
        IEnumerable<CheckoutItemDto> GetList(string userId);

        /// <summary>
        /// 쿠키의 장바구니에 담긴 상품들을 조회
        /// </summary>
        IEnumerable<CheckoutItemDto> GetList(IEnumerable<Checkout> checkoutList);

        /// <summary>
        /// 장바구니에 해당 상품을 추가 또는 업데이트
        /// </summary>
        void UpsertItem(string userId, long productId, int productCount);

        /// <summary>
        /// 여러 상품을 DB에 Insert
        /// </summary>
        int InsertItems(string userId, IEnumerable<Checkout> checkoutList);

        /// <summary>
        /// 장바구니에 등록된 상품을 삭제
        /// </summary>
        void DeleteItems(string userId, List<long> productIdList);

    }
}
