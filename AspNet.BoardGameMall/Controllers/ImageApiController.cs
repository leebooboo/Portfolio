using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AspNet.BoardGameMall.Models;
using Portfolio.Entities;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    public class ImageApiController : ApiController
    {
        private string ImageUploadPath = StringConst.ProductImageUploadPath;
        private IImageService imageService;

        public ImageApiController(IImageService imageService)
        {
            this.imageService = imageService;
        }
                
        [HttpPost]
        public async Task<JsonResult<ResultJson>> Upload(int productId)
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                return Json(new ResultJson(false, null, "MIME 다중 파트 콘텐츠가 아닙니다."));
            }

            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath(ImageUploadPath);
            var provider = new MultipartFormDataStreamProvider(root);

            Dictionary<string, string> localFiles = new Dictionary<string, string>();
            
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                if(!provider.FormData.AllKeys.Contains("ImageUseTypeId"))
                {
                    throw new Exception("요청 Body에 ImageUseTypeId 키가 없습니다.");
                }

                int imageUseTypeId = Int32.Parse(provider.FormData["ImageUseTypeId"]);

                if(provider.FileData.Count == 0)
                {
                    return Json(new ResultJson(false, null, "다중 파트 파일 데이터가 존재하지 않습니다."));
                }

                ProductImagesDto dto = new ProductImagesDto
                {
                    ProductId = productId,
                    ImageUseTypeId = imageUseTypeId,
                    ServerPath = ImageUploadPath,
                    ProductImageList = new List<ProductImageDto>()
                };

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;

                    name = name.Trim('"');

                    string fileName = name; //파일명 img1001.jpg

                    string localFileName = file.LocalFileName;

                    localFiles.Add(fileName, localFileName); // 서비스에서 db에 저장 완료/실패 의 경우에 파일을 처리하기 위해 딕셔너리 생성

                    if(!file.Headers.ContentType.MediaType.Contains("image"))
                    {
                        throw new Exception("요청에 이미지 파일이 아닌 파일이 있습니다.");
                    }

                    FileInfo fileInfo = new FileInfo(localFileName);

                    ProductImageDto image = new ProductImageDto {
                        ImageName = fileName,
                        ImageSize = (int)fileInfo.Length,
                        ImageType = fileName.Split('.').Last()
                    };

                    dto.ProductImageList.Add(image);                  
                }

                imageService.InsertProductImages(dto);

                foreach (var item in localFiles)
                {
                    string imagePath = dto.ProductImageList.First(x => x.ImageName == item.Key).ImagePath;                    
                    var filePath = Path.Combine(root, Path.GetFileName(imagePath));
                    // BodyPart_xxxx 형식으로 임시 저장된 파일을 파일명을 변경
                    File.Move(item.Value, filePath);
                }
                
                return Json(new ResultJson(true));
            }
            catch (Exception ex)
            {
                foreach (var item in localFiles)
                {
                    // BodyPart_xxxx 형식으로 임시 저장된 파일을 삭제
                    File.Delete(item.Value);
                }

                return Json(new ResultJson(false, null, ex.Message));
            }

        }
    }
}
