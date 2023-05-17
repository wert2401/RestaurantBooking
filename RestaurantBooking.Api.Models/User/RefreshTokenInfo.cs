namespace RestaurantBooking.Api.Models.User
{
    public class RefreshTokenInfo
    {
        public string? RefreshToken { get; set; }
        public DateTime ExpiringTime { get; set; }
    }
}
