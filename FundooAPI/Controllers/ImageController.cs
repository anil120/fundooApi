using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooAPI.PhotoHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageHandler _imageHandler;
        public ImageController(IImageHandler imageHandler)
        {
            _imageHandler = imageHandler;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try {
                return await _imageHandler.UploadImage(file);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
         }

    }
}