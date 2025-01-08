using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra3IDWMBackend.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Catedra3IDWMBackend.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            using (var stream = image.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            }
        }
    }
}