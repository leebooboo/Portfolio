namespace Portfolio.Services.DTO
{
    public class UploadImageDto
    {
        public string ServerPath { get; set; }
        public long ProductId { get; set; }
        public int ImageUseTypeId { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public int ImageSize { get; set; }
        public string ImagePath { get; set; }
    }
}
