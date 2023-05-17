using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RestaurantService
{
    public interface IRestaurantService
    {
        void Add(Restaurant newModel);
        IQueryable<Restaurant> GetAll();
        Restaurant GetById(int id);
        void Patch(Restaurant newRestaurantModel);
        void Rate(Review review);
        ICollection<Restaurant> GetFavoritesbyUserId(int userId);
        void AddToFavorites(int userId, int restaurantId);
        void RemoveFromFavorites(int userId, int restaurantId);
    }
}