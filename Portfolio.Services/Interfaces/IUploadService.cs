using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolio.Services.DTO;

namespace Portfolio.Services.Interfaces
{
    public interface IUploadService : IDisposable
    {
        Task<UploadImageDto> UploadImage();
    }
}
