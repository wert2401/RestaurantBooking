using Microsoft.AspNetCore.Http;

namespace RestaurantBooking.Application.Services.ImagesService
{
    public interface IImageService
    {
        string SaveImage(IFormFile img);
    }
}