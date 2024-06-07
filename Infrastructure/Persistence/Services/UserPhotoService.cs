using Application.Persistence.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence.Services
{
    public class UserPhotoService : IUserPhotoService
    {
        private readonly IWebHostEnvironment _environment;

        public UserPhotoService(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }
        public string SaveImage(IFormFile imageFile)
        {
            var contentPath = _environment.ContentRootPath;
            // path = "c://projects/productminiapi/uploads" ,not exactly something like that
            var path = Path.Combine(contentPath, "UserPhotos");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check the allowed extenstions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                throw new Exception(msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            // we are trying to create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            imageFile.CopyTo(stream);
            stream.Close();
            return newFileName;
        }

        public bool DeleteImage(string imageFileName)
        {
            var contentPath = this._environment.ContentRootPath;
            var path = Path.Combine(contentPath, $"UserPhotos", imageFileName);
            if (File.Exists(path))
                File.Delete(path);

            if (File.Exists(path))
                return false;

            return true;
        }
    }
}
