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
        [Required]
        public int Capacity { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public bool IsClaimed { get; set; }
    }
}
