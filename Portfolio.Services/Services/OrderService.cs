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
using System.Data.Entity;

namespace Portfolio.Services.Services
{
    public class OrderService : IOrderService
    {
        private PortfolioContext db;

        public OrderService(PortfolioContext context)
        {
            db = context;
        }

        public void AddOrder(List<CheckoutItemDto> products, string userId)
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            var todayOrderCount = db.Orders.Where(x => x.OrderNo.StartsWith(today)).Count();
            string maxOrderNo;

            if(todayOrderCount > 0)
                maxOrderNo = db.Orders.Where(x => x.OrderNo.StartsWith(today)).Max(x => x.OrderNo).Substring(8);
            else
                maxOrderNo = "00000";

            int no = Int32.Parse(maxOrderNo) + 1;
            string nowOrderNo = today + no.ToString().PadLeft(5, '0');

            decimal totalPrice = 0m;
            products.ForEach(x => totalPrice += x.Price * x.ProductCount);

            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    var orderNo = db.Orders.Where(x => x.OrderNo.StartsWith(today)).Max(x => x.OrderNo);

                    Order order = new Order
                    {
                        OrderNo = nowOrderNo,
                        UserId = userId,
                        OrderTypeId = (int)OrderTypeEnum.주문완료,
                        InsertDt = DateTime.Now,
                        TotalPrice = totalPrice
                    };

                    db.Orders.Add(order);

                    db.SaveChanges();

                    order.OrderDetails = new List<OrderDetail>();

                    foreach (var item in products)
                    {
                        OrderDetail detail = new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = item.ProductId,
                            Price = item.Price,
                            Count = item.ProductCount,
                            SumPrice = item.Price * item.ProductCount,
                            InsertDt = DateTime.Now
                        };

                        order.OrderDetails.Add(detail);
                    }

                    db.SaveChanges();

                    DeleteCheckoutItems(products, userId);

                    tran.Commit();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"주문 추가 오류 발생:\r\n{ex.Message}");
                    tran.Rollback();
                    throw ex;
                }
            }
        }

        public List<Order> GetOrderList(string userId, DateTime startDt, DateTime endDt)
        {
            endDt = new DateTime(endDt.Year, endDt.Month, endDt.Day, 23, 59, 59);
            var list = db.Orders.Include("OrderType")
                                .Where(x => x.UserId == userId && x.InsertDt >= startDt && x.InsertDt <= endDt)
                                .OrderByDescending(x => x.OrderNo)
                                .ToList();
            return list;
        }

        public Order GetOrder(long orderId, string userId)
        {
            db.Database.Log = x => Debug.WriteLine(x);

            var order = db.Orders.Include("OrderType")
                                 .Include(x => x.OrderDetails.Select(y => y.Product))
                                 .FirstOrDefault(x => x.OrderId == orderId && x.UserId == userId);
            
            List<long> productIdList = order.OrderDetails.Select(x => x.Product.ProductId).ToList(); //상세 주문의 productId 리스트 생성

            //상세 주문의 productID 에 해당하는 메인섬네일 ProductImage 리스트 생성
            var productMainImages = db.ProductImages.Where(x => productIdList.Contains(x.ProductId) && x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일).ToList();

            foreach (var item in order.OrderDetails.Select(x => x.Product)) //주문의 하위 상세 주문에 메인섬네일 바인딩(메인 섬네일 없는 경우 기본 Empty이미지 바인딩)
            {
                if (productMainImages.FirstOrDefault(x => x.ProductId == item.ProductId) != null)
                    item.ProductMainImagePath = productMainImages.First(x => x.ProductId == item.ProductId).ImagePath;
                else
                    item.ProductMainImagePath = StringConst.EmptyImagePath;
            }

            if (order == null)
                throw new Exception("일치하는 주문내역이 없습니다.");

            return order;
        }

        /// <summary>
        /// 주문완료된 상품은 장바구니 db에서 삭제 처리
        /// </summary>
        private void DeleteCheckoutItems(List<CheckoutItemDto> products, string userId)
        {
            List<long> productIdList = products.Select(y => y.ProductId).ToList();

            var checkouts = db.Checkouts.Where(x => x.UserId == userId && productIdList.Contains(x.ProductId));
            db.Checkouts.RemoveRange(checkouts);
            db.SaveChanges();
        }
    }
}
