using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int OwnerUserId { get; set; }
        public string? MenuPath { get; set; }
        public string? SchemeImage { get; set; }
        public string? RestaurantImage { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = null!;

        public TimeSpan OpenFrom { get; set; }
        public TimeSpan OpenTo { get; set; }

        public int TablesCount { get; set; }

        public virtual ICollection<Table> Tables { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<User> FavoritedBy { get; set; }
    }
}
