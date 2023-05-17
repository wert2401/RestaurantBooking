namespace RestaurantBooking.Application.Services.Authentication
{
    public class JwtOptions
    {
        public string ValidAudience { get; set; } = null!;
        public string ValidIssuer { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
