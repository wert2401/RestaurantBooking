using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace RestaurantBooking.Application.Services.FilesService
{
    internal class FileService : IFileService
    {
        private readonly string fullPathImageFolder;
        private readonly string localImageFolder;

        private readonly string fullPathMenuFolder;
        private readonly string localMenuFolder;

        public FileService(IOptions<FileOptions> options, IWebHostEnvironment environment)
        {
            localImageFolder = options.Value.ImageFolder;
            fullPathImageFolder = Path.Combine(environment.WebRootPath, localImageFolder);

            localMenuFolder = options.Value.MenuFolder;
            fullPathMenuFolder = Path.Combine(environment.WebRootPath, localMenuFolder);

            if (!Directory.Exists(fullPathImageFolder))
                Directory.CreateDirectory(fullPathImageFolder);

            if (!Directory.Exists(fullPathMenuFolder))
                Directory.CreateDirectory(fullPathMenuFolder);
        }

        public string SaveImage(IFormFile img)
        {
            return SaveFile(img, fullPathImageFolder, localImageFolder, ".jpg");
        }

        public string SaveMenu(IFormFile menu)
        {
            return SaveFile(menu, fullPathMenuFolder, localMenuFolder, ".pdf");
        }

        private string SaveFile(IFormFile file, string fullFolder, string localFolder, string format)
        {
            string fileName = Guid.NewGuid().ToString() + format;
            string filePath = Path.Combine(fullFolder, fileName);

            file.CopyTo(new FileStream(filePath, FileMode.Create));

            return Path.Combine(localFolder, fileName);
        }
    }
}
