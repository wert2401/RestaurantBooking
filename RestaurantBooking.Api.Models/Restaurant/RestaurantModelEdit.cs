using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Restaurant
{
    public class RestaurantModelEdit : BaseModel
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public List<string>? PhoneNumbers { get; set; }
        [Required]
        public string Address { get; set; } = null!;
    }
}
