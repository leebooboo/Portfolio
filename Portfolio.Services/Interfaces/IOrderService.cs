using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;

namespace Portfolio.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// 주문 생성(Order & List<OrderDetail> Insert) 및 동일한 상품 장바구니에서 삭제
        /// </summary>
        void AddOrder(List<CheckoutItemDto> products, string userId);

        /// <summary>
        /// 주문내역 조회
        /// </summary>
        List<Order> GetOrderList(string userId, DateTime startDt, DateTime endDt);

        /// <summary>
        /// 주문 조회
        /// </summary>
        Order GetOrder(long orderId, string userId);
    }
}
