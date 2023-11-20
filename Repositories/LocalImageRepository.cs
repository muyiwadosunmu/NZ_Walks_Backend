using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks_API.Data;
using NZWalks_API.Models.Domain;

namespace NZWalks_API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZ_Walks_DB_Context _dBContext;
        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, NZ_Walks_DB_Context dBContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._dBContext = dBContext;
        }
        public async Task<Image> Upload(Image image)
        {
            // we'll get the filepath with the help of web hosting env
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,
            "Images", $"{image.FileName}{image.FileExtension}");

            // Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://1234/images/image.jpg
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to the Images table
            await _dBContext.Images.AddAsync(image);
            await _dBContext.SaveChangesAsync();

            return image;


        }
    }
}