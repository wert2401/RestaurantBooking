using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(UserId), nameof(RestaurantId), IsUnique = true)]
    public class Review : BaseEntity
    {
        [Range(0, 5)]
        public int Grade { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RestaurantId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Restaurant Restaurant { get; set; } = null!;
    }
}
