using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;
using Portfolio.Services.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.BoardGameMall.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var products = new List<Product>
            {
                new Product{ ProductId = 1, CategoryId = 1, ProductName = "첫번째 상품", Price = 10000 },
                new Product{ ProductId = 2, CategoryId = 2, ProductName = "두번째 상품", Price = 20000, PromotionPrice = 18000 },
                new Product{ ProductId = 3, CategoryId = 1, ProductName = "세번째 상품", Price = 20000 }
            };

            var imageUseTypes = new List<ImageUseType>();
            foreach (ImageUseTypeEnum item in Enum.GetValues(typeof(ImageUseTypeEnum)))
            {
                imageUseTypes.Add(new ImageUseType { ImageUseTypeId = (int)item, TypeName = item.ToString() });
            }

            var orderTypes = new List<OrderType>();
            foreach (OrderTypeEnum item in Enum.GetValues(typeof(OrderTypeEnum)))
            {
                orderTypes.Add(new OrderType { OrderTypeId = (int)item, Type = item.ToString() });
            }

            var productImage = new ProductImage
            {
                ProductId = 1,
                ImageName = "FirstImage.png",
                ImageUseTypeId = (int)ImageUseTypeEnum.메인섬네일,
                ImagePath = "~/Images/Products/1_T1.png",
                ImageSize = 1024,
                ImageType = "png",
                InsertDt = DateTime.Now
            };

            var orders = new List<Order>
            {
                new Order
                {
                    OrderNo = "2020051900001",
                    UserId = "c8429f19",
                    TotalPrice = 66000,
                    OrderTypeId = (int)OrderTypeEnum.주문완료,
                    InsertDt = new DateTime(2020, 5, 19, 23, 0, 0),
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 1,
                            Count = 3,
                            Price = 10000,
                            SumPrice = 30000,
                            InsertDt = DateTime.Now
                        },
                        new OrderDetail
                        {
                            ProductId = 2,
                            Count = 2,
                            Price = 18000,
                            SumPrice = 36000,
                            InsertDt = DateTime.Now
                        }
                    }
                },
                new Order
                {
                    OrderNo = "2020051800001",
                    UserId = "c8429f19",
                    TotalPrice = 28000,
                    OrderTypeId = (int)OrderTypeEnum.주문완료,
                    InsertDt = new DateTime(2020, 5, 18, 2, 0, 0),
                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductId = 1,
                            Count = 1,
                            Price = 10000,
                            SumPrice = 10000,
                            InsertDt = DateTime.Now
                        }
                    }
                },

            };

            context.Products.AddRange(products);
            context.ImageUseTypes.AddRange(imageUseTypes);
            context.OrderTypes.AddRange(orderTypes);
            context.ProductImages.Add(productImage);            
            context.Orders.AddRange(orders);
            
            context.SaveChanges();
        }

        [TestMethod]
        public void AddOrder()
        {
            string userId = "c8429f19";
            string today = DateTime.Now.ToString("yyyyMMdd");

            var products = new List<CheckoutItemDto>
            {
                new CheckoutItemDto
                {
                    ProductId = 1,
                    ProductCount = 2,
                    Price = 10000
                },
                new CheckoutItemDto
                {
                    ProductId = 2,
                    ProductCount = 4,
                    Price = 15000
                },
                new CheckoutItemDto
                {
                    ProductId = 3,
                    ProductCount = 10,
                    Price = 10000
                }
            };

            var service = new OrderService(context);
            service.AddOrder(products, userId);

            var result = context.Orders.Where(x => x.UserId == userId && x.OrderNo.StartsWith(today)).ToList();
            var details = result[0].OrderDetails.ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(180000, result[0].TotalPrice);
            Assert.AreEqual(3, details.Count);
        }

        [TestMethod]
        public void GetOrderList()
        {
            string userId = "c8429f19";
            DateTime startDt = new DateTime(2020, 5, 18);
            DateTime endDt = new DateTime(2020, 5, 19);

            var service = new OrderService(context);
            var results_2Day = service.GetOrderList(userId, startDt, endDt);

            Assert.AreEqual(2, results_2Day.Count);
            Assert.AreEqual(2, results_2Day[0].OrderDetails.Count);

            startDt = new DateTime(2020, 5, 18);
            endDt = new DateTime(2020, 5, 18);

            var results_1Day = service.GetOrderList(userId, startDt, endDt);

            Assert.AreEqual(1, results_1Day.Count);
            Assert.AreEqual(1, results_1Day[0].OrderDetails.Count);
        }

        [TestMethod]
        public void GetOrder()
        {
            long orderId = 1;
            string userId = "c8429f19";

            var service = new OrderService(context);
            var result = service.GetOrder(orderId, userId);

            var details = result.OrderDetails.ToList();

            Assert.AreEqual(orderId, result.OrderId);
            Assert.AreEqual(66000, result.TotalPrice);
            Assert.AreEqual(2, details.Count);
            Assert.AreEqual(30000, details[0].SumPrice);
            Assert.AreEqual(36000, details[1].SumPrice);
        }


    }
}
