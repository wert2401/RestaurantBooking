using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity
    {
        [MaxLength(200)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        [MaxLength(128)]
        public string PasswordHash { get; set; } = null!;
        [MaxLength(50)]
        public string? Phone { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiring { get; set; }

        public List<Role> Roles { get; set; } = new();
        public List<Restaurant> FavoriteRestaurants { get; set; } = new();
    }
}
