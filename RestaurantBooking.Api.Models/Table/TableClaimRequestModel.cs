using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Table
{
    public class TableClaimRequestModel
    {
        [Required]
        public int TableId { get; set; }
        [Required]
        public DateTime ClaimFromDate { get; set; }
        [Required]
        public DateTime ClaimToDate { get; set; }
    }
}
