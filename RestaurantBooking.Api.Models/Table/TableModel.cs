using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Table
{
    public class TableModel : BaseModel
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        public string? Restaurant { get; set; } = null;
        public bool IsClaimed { get; set; }
    }
}
