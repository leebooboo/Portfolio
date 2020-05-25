using System.Collections.Generic;
using Portfolio.Entities.Models;

namespace Portfolio.Services.DTO
{
    public class ProductViewDto
    {
        public Product Product { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}
