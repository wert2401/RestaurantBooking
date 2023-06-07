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
        public bool IsCanceled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClaimFromDate { get; set; }
        public DateTime ClaimToDate { get; set; }
    }
}
