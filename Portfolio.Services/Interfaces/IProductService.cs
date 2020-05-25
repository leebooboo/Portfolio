using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;
using Portfolio.Entities.Models;

namespace Portfolio.Services.Interfaces
{
    public interface IProductService
    {
        ///// <summary>
        ///// 전달되는 이미지 정보를 ProductImages 테이블에 Insert 하고, 이미지 파일명을 ProductImageId 로 변환해서 이미지 파일을 서버에 저장한다.
        ///// DB Insert 와 이미지 파일 저장은 트랜잭션 처리되어 트랜잭션을 보장한다.
        ///// </summary>
        //void SaveImage(ProductImageDto dto);

        IEnumerable<Product> GetCategoryProducts(int categoryId);

        IEnumerable<Product> GetProducts(string productName);

        IEnumerable<Product> GetProducts(int page = 1, int count = 20);

        ProductViewDto GetProduct(long productId);

    }
}
