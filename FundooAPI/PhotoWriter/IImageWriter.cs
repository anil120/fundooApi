using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FundooAPI.PhotoWriter
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file);
    }
}
