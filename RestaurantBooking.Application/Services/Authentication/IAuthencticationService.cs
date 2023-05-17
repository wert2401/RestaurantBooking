using RestaurantBooking.Application.Services.Authentication.Models;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticatedModel? AuthUser(LoginModel loginModel);
        AuthenticatedModel RefreshToken(TokenRefreshModel tokenRefreshModel);
        User RegisterUser(RegisterModel registerModel, bool isAdmin = false);
    }
}
