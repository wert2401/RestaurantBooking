namespace RestaurantBooking.Application.Services.Authentication.Models
{
    public class TokenRefreshModel
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
