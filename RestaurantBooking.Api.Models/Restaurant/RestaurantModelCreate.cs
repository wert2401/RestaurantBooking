using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Restaurant
{
    public class RestaurantModelCreate
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        public IFormFile? MainPhoto { get; set; }
        public List<string>? PhoneNumbers { get; set; }
        [Required]
        [MaxLength(500)]
        public string Address { get; set; } = null!;

        [Required]
        public TimeSpan OpenFrom { get; set; }
        [Required]
        public TimeSpan OpenTo { get; set; }
    }
}
