using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Review
{
    public class ReviewModelCreate
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        [Range(0, 5)]
        public int Grade { get; set; }
    }
}
