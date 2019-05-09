using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooAPI.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public CloudinaryController(AuthenticationContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("profile/{email}")]
        public string Upload(IFormFile file, string email)
        {
            try
            {
                if (file == null)
                {
                    return "no file found";
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dve38i9wc", "266748281619115", "jeORD7Xyb0RuVjzbxUZU5pVpdbg");

                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                var data = this._context.ApplicationUsers.Where(t => t.Email == email).FirstOrDefault();
                data.ProfilePic = uploadResult.Uri.ToString();
                int result = 0;
                try
                {
                    result = this._context.SaveChanges();
                    return data.ProfilePic;
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [Route("notes/{email}")]
        public string NotesImage(IFormFile file, string email)
        {
            try
            {
                if (file == null)
                {
                    return "no file found";
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("dve38i9wc", "266748281619115", "jeORD7Xyb0RuVjzbxUZU5pVpdbg");

                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                var data = this._context.ApplicationUsers.Where(t => t.Email == email).FirstOrDefault();
                data.ProfilePic = uploadResult.Uri.ToString();
                int result = 0;
                try
                {
                    result = this._context.SaveChanges();
                    return data.ProfilePic;
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
