using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace RestaurantBooking.Application.Services.ImagesService
{
    internal class ImageService : IImageService
    {
        private readonly string fullPathFolder;
        private readonly string localFolder;

        public ImageService(IOptions<ImageOptions> options, IWebHostEnvironment environment)
        {
            localFolder = options.Value.ImageFolder;
            fullPathFolder = Path.Combine(environment.WebRootPath, localFolder);

            if (!Directory.Exists(fullPathFolder))
                Directory.CreateDirectory(fullPathFolder);
        }

        public string SaveImage(IFormFile img)
        {
            return SaveImg(img, fullPathFolder);
        }

        private string SaveImg(IFormFile img, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = Path.Combine(fullPathFolder, fileName);
            img.CopyTo(new FileStream(filePath, FileMode.Create));

            return Path.Combine(localFolder, fileName);
        }
    }
}
