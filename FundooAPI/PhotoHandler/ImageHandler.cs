﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooAPI.PhotoWriter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.PhotoHandler
{
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return new ObjectResult(result);
        }
    }
}
