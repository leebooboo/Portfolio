using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entities.Enums
{
    public enum ImageUseTypeEnum
    {
        메인섬네일 = 1,
        서브섬네일,
        상품세부이미지,
        상품문의이미지,
        상품후기이미지
    }

    public enum OrderTypeEnum
    {
        주문완료 = 1,
        결제대기,
        결제완료,
        배송중,
        배송완료
    }
    
    public enum CategoryEnum
    {
        전략 = 1,
        가족,
        파티,
        테마
    }

}
