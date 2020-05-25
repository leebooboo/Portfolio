using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Models;
using Portfolio.Entities.Enums;

namespace Portfolio.Services.Services
{
    public class CheckoutService : ICheckoutService
    {
        private PortfolioContext db;

        public CheckoutService(PortfolioContext context)
        {
            db = context;
        }

        public IEnumerable<CheckoutItemDto> GetList(string userId)
        {
            var checkoutList = db.Checkouts.Where(x => x.UserId == userId)
                                   .OrderBy(x => x.ProductId)
                                   .ToList();

            var list = GetList(checkoutList);

            return list;
        }

        //checkoutList를 전달받아 IEnumerable<CheckoutItemDto>로 변환해서 반환
        public IEnumerable<CheckoutItemDto> GetList(IEnumerable<Checkout> checkoutList)
        {
            List<long> productIdList = checkoutList.Select(y => y.ProductId).ToList();

            var list = (from product in db.Products.Where(x => productIdList.Contains(x.ProductId))
                       join image in db.ProductImages.Where(x => x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일)
                       on product.ProductId equals image.ProductId into g
                       from pImage in g.DefaultIfEmpty()
                       orderby product.ProductId
                       select new CheckoutItemDto
                       {
                           ProductId = product.ProductId,
                           ProductName = product.ProductName,
                           Price = product.PromotionPrice ?? product.Price,                           
                           MainImagePath = pImage.ImagePath ?? StringConst.EmptyImagePath
                       }).ToList();
            
            foreach (var item in list)
            {
                item.ProductCount = checkoutList.First(x => x.ProductId == item.ProductId).ProductCount;
            }

            return list.ToList();
        }
                
        public void UpsertItem(string userId, long productId, int productCount)
        {
            var product = db.Checkouts.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);
            if(product == null)
            {
                db.Checkouts.Add(new Checkout()
                {
                    UserId = userId,
                    ProductId = productId,
                    ProductCount = productCount,
                    InsertDt = DateTime.Now
                });
            }
            else
            {
                product.ProductCount = productCount;
                product.UpdateDt = DateTime.Now;
            }

            db.SaveChanges();
        }

        public int InsertItems(string userId, IEnumerable<Checkout> checkoutList)
        {
            foreach (var item in checkoutList)
            {
                db.Checkouts.Add(new Checkout()
                {
                    UserId = userId,
                    ProductId = item.ProductId,
                    ProductCount = item.ProductCount,
                    InsertDt = DateTime.Now
                });
            }

            return db.SaveChanges();
        }

        public void DeleteItems(string userId, List<long> productIdList)
        {
            var checkouts = db.Checkouts.Where(x => x.UserId == userId && productIdList.Contains(x.ProductId)).ToList();

            foreach (var item in checkouts)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }

            db.SaveChanges();
        }
    }
}
