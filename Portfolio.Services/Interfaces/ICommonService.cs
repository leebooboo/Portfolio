using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;

namespace Portfolio.Services.Interfaces
{
    internal interface ICommonService
    {
        /// <summary>
        /// 상품 선택 드롭다운에 사용할 드롭다운 리스트를 반환
        /// </summary>
        List<ProductDropdown> GetProductList();
    }
}
