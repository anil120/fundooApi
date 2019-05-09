using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.CloudinaryServices
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(string apiKey = "266748281619115", string apiSecret = "jeORD7Xyb0RuVjzbxUZU5pVpdbg", string cloudName = "dve38i9wc")
        {
            var myAccount = new Account { ApiKey = apiKey, ApiSecret = apiSecret, Cloud = cloudName };
            _cloudinary = new Cloudinary(myAccount);
        }
        public ImageUploadResult UploadImage(IFormFile file)
        {
            if (file != null)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Transformation = new Transformation().Width(200).Height(200).Crop("thumb").Gravity("face")
                };

                var uploadResult = _cloudinary.Upload(uploadParams);
                return uploadResult;
            }
            return null;
        }
        public void DeleteResource(string publicId)
        {
            var delParams = new DelResParams()
            {
                 PublicIds = new List<string>()
                { publicId }, Invalidate = true };
                 _cloudinary.DeleteResources(delParams);
            }
    }

}

