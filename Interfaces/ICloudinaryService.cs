using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra3IDWMBackend.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile image);
    }
}
