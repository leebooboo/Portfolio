using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Entities.Models;

namespace Portfolio.Services.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// categoryId와 일치하는 카테고리 조회
        /// </summary>
        /// <returns></returns>
        Category GetCategory(int categoryId);

        /// <summary>
        /// 모든 카테고리 조회
        /// </summary>
        IEnumerable<Category> GetCategoryList();
    }
}
