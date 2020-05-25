using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Models;
using System.Diagnostics;
using Portfolio.Entities.Enums;

namespace Portfolio.Services.Services
{
    public class ProductService : IProductService
    {
        private PortfolioContext db;

        public ProductService(PortfolioContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetCategoryProducts(int categoryId)
        {
            var products = db.Products.Where(x => x.CategoryId == categoryId);

            return GetProductsCore(products);
        }
        
        public IEnumerable<Product> GetProducts(string productName)
        {
            var products = db.Products.Where(x => x.ProductName.Contains(productName)).OrderBy(x => x.ProductName).AsQueryable();

            return GetProductsCore(products);
        }

        public IEnumerable<Product> GetProducts(int page = 1, int count = 20)
        {
            var products = db.Products.OrderBy(x => x.ProductId).Skip((page - 1) * count).Take(count).AsQueryable();

            return GetProductsCore(products);
        }

        private List<Product> GetProductsCore(IQueryable<Product> products)
        {
            db.Database.Log = x => Debug.WriteLine(x);

            var list = ((from product in products
                       join image in db.ProductImages.Where(x => x.ImageUseTypeId == (int)ImageUseTypeEnum.메인섬네일).GroupBy(x => x.ProductId, (key, x) => x.FirstOrDefault())
                       on product.ProductId equals image.ProductId into g
                       from pImage in g.DefaultIfEmpty()
                       select new {
                           Product = product,
                           Image = pImage.ImagePath ?? StringConst.EmptyImagePath
                       })
                       .AsEnumerable()
                       .Select(x => {
                           x.Product.ProductMainImagePath = x.Image;
                           return x.Product;
                        })).ToList();
            
            return list;
        }

        public ProductViewDto GetProduct(long productId)
        {
            ProductViewDto dto = new ProductViewDto();

            dto.Product = db.Products.FirstOrDefault(x => x.ProductId == productId);

            if (dto.Product == null)
                throw new Exception("일치하는 상품을 찾을 수 없습니다.");

            dto.ProductImages = db.ProductImages.Where(x => x.ProductId == productId).OrderBy(x => x.ImageUseTypeId).ThenBy(x => x.ImagePath).ToList();

            return dto;
        }

          

    }
}
