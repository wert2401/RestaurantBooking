using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Table
{
    public class TableClaimModel : BaseModel
    {
        public int TableId { get; set; }
        public int UserId { get; set; }
        public int TableNumber { get; set; }
        public string Restaurant { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserPhoneNumber { get; set; } = string.Empty;
        public string RestaurantPhoneNumber { get; set; } = string.Empty;
        public bool IsExpired { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClaimFromDate { get; set; }
        public DateTime ClaimToDate { get; set; }
    }
}
