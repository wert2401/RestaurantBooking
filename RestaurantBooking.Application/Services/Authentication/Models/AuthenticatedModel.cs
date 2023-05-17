namespace RestaurantBooking.Application.Services.Authentication.Models
{
    public class AuthenticatedModel
    {
        public AuthenticatedModel(string token, string refreshToken, DateTime validTo)
        {
            Token = token;
            RefreshToken = refreshToken;
            ValidTo = validTo;
        }

        public string Token { get; }
        public string RefreshToken { get; }
        public DateTime ValidTo { get; }
    }
}