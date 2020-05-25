using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Models;
using System.Diagnostics;

namespace Portfolio.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private PortfolioContext db;

        public CategoryService(PortfolioContext context)
        {
            db = context;
        }
            
        public Category GetCategory(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category == null)
                throw new Exception($"ID가 {categoryId}인 카테고리를 찾을 수 없습니다.");

            return category;
        }

        public IEnumerable<Category> GetCategoryList()
        {
            var categories = db.Categories.OrderBy(x => x.Order).ToList();
            return categories;
        }
    }
}
