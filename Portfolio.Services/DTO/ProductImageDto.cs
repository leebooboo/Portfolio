using System.Collections.Generic;

namespace Portfolio.Services.DTO
{
    public class ProductImagesDto
    {
        public string ServerPath { get; set; }
        public long ProductId { get; set; }
        public int ImageUseTypeId { get; set; }
        public List<ProductImageDto> ProductImageList { get; set; }
    }

    public class ProductImageDto
    {        
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public int ImageSize { get; set; }
        public string ImagePath { get; set; }
    }
}
