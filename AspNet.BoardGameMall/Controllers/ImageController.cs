using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet.BoardGameMall.Models;
using Portfolio.Entities;
using Portfolio.Services.DTO;
using Portfolio.Services.Interfaces;

namespace AspNet.BoardGameMall.Controllers
{
    public class ImageController : Controller
    {
        private string ImageUploadPath = StringConst.UploadImageUploadPath;
        private IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        /// <summary>
        /// CkEditor4 에서 이미지 업로드시 실행되는 액션
        /// 이미지 업로드 완료되면 UploadImageResponse 을 통해서 해당 이미지의 url을 리턴
        /// </summary>
        [HttpPost]
        public JsonResult UploadImage(long productId, int imageUseType)
        {
            try
            {
                if (Request.Files.Count == 0)
                    throw new Exception("요청에 파일이 없습니다.");

                if (Request.Files[0].ContentLength <= 0)
                    throw new Exception("이미지의 크기가 올바르지 않습니다.");

                if (!Request.Files[0].ContentType.Contains("image"))
                    throw new Exception("이미지 파일만 업로드 할 수 있습니다.");

                HttpPostedFileBase file = Request.Files[0];

                string fileName = Path.GetFileName(file.FileName);
                                
                UploadImageDto dto = new UploadImageDto
                {
                    ProductId = productId,
                    ImageName = fileName,
                    ImageUseTypeId = imageUseType,
                    ServerPath = ImageUploadPath,
                    ImageType = fileName.Split('.').Last(),
                    ImageSize = file.ContentLength
                };

                imageService.InsertUploadImage(dto);

                string serverPath = Server.MapPath(dto.ImagePath);

                file.SaveAs(serverPath);

                return Json(new UploadImageResponse { uploaded = 1, fileName = fileName, url = Url.Content(dto.ImagePath) });
            }
            catch(Exception ex)
            {
                return Json(new UploadImageResponse
                {
                    uploaded = 0,
                    error = new Models.UploadImageError($"이미지 업로드 처리중에 오류가 발생했습니다.\r\n{ex.Message}")
                });
            }      
        }

    }
}