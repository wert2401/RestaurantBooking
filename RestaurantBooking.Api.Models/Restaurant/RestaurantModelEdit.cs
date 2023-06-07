using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Restaurant
{
    public class RestaurantModelEdit
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public List<string>? PhoneNumbers { get; set; }
        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public TimeSpan OpenFrom { get; set; }
        [Required]
        public TimeSpan OpenTo { get; set; }
    }
}
