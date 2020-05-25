using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Portfolio.Entities;
using Portfolio.Entities.Enums;
using Portfolio.Entities.Models;
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
    public class CategoryServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var categories = new List<Category>
            {
                new Category{ CategoryId = 1, CategoryName = "전략", Order = 4 },
                new Category{ CategoryId = 2, CategoryName = "가족", Order = 3 },
                new Category{ CategoryId = 3, CategoryName = "파티", Order = 2 },
                new Category{ CategoryId = 4, CategoryName = "테마", Order = 1 }
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        [TestMethod]
        public void GetCategory()
        {
            var service = new CategoryService(context);

            var category2 = service.GetCategory(2);

            Assert.AreNotEqual(null, category2);
            Assert.AreEqual(2, category2.CategoryId);
            Assert.AreEqual("가족", category2.CategoryName);
            Assert.AreEqual(3, category2.Order);

            var category4 = service.GetCategory(4);

            Assert.AreNotEqual(null, category4);
            Assert.AreEqual(4, category4.CategoryId);
            Assert.AreEqual("테마", category4.CategoryName);
            Assert.AreEqual(1, category4.Order);
        }

        [TestMethod]
        public void GetCategoryList()
        {
            var service = new CategoryService(context);

            var categories = service.GetCategoryList().ToList();

            Assert.AreEqual(4, categories.Count);
            Assert.AreEqual("테마", categories[0].CategoryName);
            Assert.AreEqual("파티", categories[1].CategoryName);
            Assert.AreEqual("가족", categories[2].CategoryName);
            Assert.AreEqual("전략", categories[3].CategoryName);
        }


    }
}
