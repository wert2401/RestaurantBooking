using Microsoft.AspNetCore.Http;

namespace RestaurantBooking.Application.Services.FilesService
{
    public interface IFileService
    {
        string SaveImage(IFormFile img);
        string SaveMenu(IFormFile menu);
    }
}