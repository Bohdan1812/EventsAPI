using Microsoft.AspNetCore.Http;

namespace Application.Persistence.Services
{
    public interface IEventPhotoService
    {
        string SaveImage(IFormFile imageFile);
        bool DeleteImage(string imageFileName);
    }
}
