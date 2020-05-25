using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Entities;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services.Services
{
    internal class CommonService : ICommonService
    {
        private PortfolioContext db;

        internal CommonService(PortfolioContext context)
        {
            db = context;
        }

        //상품 선택 드롭다운에 사용할 드롭다운 리스트를 생성
        public List<ProductDropdown> GetProductList()
        {
            var list = from product in db.Products
                       join category in db.Categories
                       on product.CategoryId equals category.CategoryId into g
                       from pCate in g.DefaultIfEmpty()
                       select new ProductDropdown
                       {
                           CategoryId = product.CategoryId,
                           CategoryName = pCate.CategoryName,
                           ProductId = product.ProductId,
                           ProductName = product.ProductName
                       };
            
            return list.OrderBy(x => x.CategoryId).ThenBy(x => x.ProductName).ToList();
        }
    }
}
