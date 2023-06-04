using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RestaurantService
{
    public interface IRestaurantService
    {
        void Add(Restaurant newModel);
        IQueryable<Restaurant> GetAll();
        Restaurant GetById(int id);
        Restaurant GetByOwnerEmail(string email);
        void Patch(Restaurant oldRest, Restaurant newRestaurantModel);
        void Rate(Review review);
    }
}