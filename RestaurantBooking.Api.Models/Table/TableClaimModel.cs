using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Table
{
    public class TableClaimModel : BaseModel
    {
        [Required]
        public int TableId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsExpired { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClaimDate { get; set; }
    }
}
