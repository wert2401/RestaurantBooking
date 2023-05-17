using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string? Description { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
