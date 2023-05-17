using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.UserService
{
    public interface IUserService
    {
        void Add(User newModel);
        User GetByEmail(string email);
        void Patch(User editModel);
        void SetRefreshToken(int id, string refreshToken, DateTime expiringTime);
        void SetRefreshToken(string email, string refreshToken, DateTime expiringTime);
        bool IsExist(string email);
        bool HasUserRestaurant(int id);
    }
}