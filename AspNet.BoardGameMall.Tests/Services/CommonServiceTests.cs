using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Portfolio.Entities;
using Portfolio.Entities.Models;
using Portfolio.Services.DTO;

namespace AspNet.BoardGameMall.Tests
{
    [TestClass]
    public class CommonServiceTests
    {
        private PortfolioContext context;

        [TestInitialize]
        public void Initialize()
        {
            context = new PortfolioContext(Effort.DbConnectionFactory.CreateTransient());

            var categories = new List<Category>
            {
                new Category{ CategoryId = 1, CategoryName = "전략", Order = 1 },
                new Category{ CategoryId = 2, CategoryName = "가족", Order = 2 },
                new Category{ CategoryId = 3, CategoryName = "파티", Order = 3 }
            };

            var products = new List<Product>
            {
                new Product{ ProductId = 1, CategoryId = 1, ProductName = "C1_P1" },
                new Product{ ProductId = 2, CategoryId = 2, ProductName = "C2_P2" },
                new Product{ ProductId = 3, CategoryId = 3, ProductName = "C3_P3" },
                new Product{ ProductId = 4, CategoryId = 1, ProductName = "C1_P4" },
                new Product{ ProductId = 5, CategoryId = 2, ProductName = "C2_P5" },
                new Product{ ProductId = 6, CategoryId = 3, ProductName = "C3_P6" },
                new Product{ ProductId = 7, CategoryId = 1, ProductName = "C1_P7" },
                new Product{ ProductId = 8, CategoryId = 2, ProductName = "C2_P8" },
                new Product{ ProductId = 9, CategoryId = 3, ProductName = "C3_P9" }
            };

            context.Categories.AddRange(categories);
            context.Products.AddRange(products);

            context.SaveChanges();
        }

        [TestMethod]
        public void GetProductList()
        {
            //CommonService는 컨트롤러에서 호출하지 않고, 서비스에서만 사용할 수 있게 internal을 사용하기 때문에 리플렉션으로 테스트
            Assembly assembly = Assembly.LoadFrom("Portfolio.Services.dll");            
            object common = assembly.CreateInstance("Portfolio.Services.Services.CommonService", 
                                                            false, 
                                                            BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Instance,
                                                            null,
                                                            new object[] { context },
                                                            null,
                                                            null);

            Type t = common.GetType();
            var methodInfo = t.GetMethods().FirstOrDefault(x => x.Name == "GetProductList");
            var result = methodInfo.Invoke(common, null) as List<ProductDropdown>;

            Assert.AreEqual(9, result.Count);
            Assert.AreEqual("C1_P1", result[0].ProductName);
            Assert.AreEqual("C1_P4", result[1].ProductName);
            Assert.AreEqual("C1_P7", result[2].ProductName);
            Assert.AreEqual("C2_P2", result[3].ProductName);
            Assert.AreEqual("C2_P5", result[4].ProductName);
            Assert.AreEqual("C2_P8", result[5].ProductName);
            Assert.AreEqual("C3_P3", result[6].ProductName);
            Assert.AreEqual("C3_P6", result[7].ProductName);
            Assert.AreEqual("C3_P9", result[8].ProductName);
        }
    }
}
