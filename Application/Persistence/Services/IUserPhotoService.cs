using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Persistence.Services
{
    public interface IUserPhotoService
    {
        string SaveImage(IFormFile imageFile);
        bool DeleteImage(string imageFileName);
    }

}
