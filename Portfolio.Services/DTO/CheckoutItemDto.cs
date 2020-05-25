using System.ComponentModel.DataAnnotations;

namespace Portfolio.Services.DTO
{
    public class CheckoutItemDto
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#,0}")]
        public decimal Price { get; set; }
        public string MainImagePath { get; set; }
        public bool IsChecked { get; set; }
    }
}
