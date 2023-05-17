using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.Table
{
    public class TableModelCreate
    {
        [Required]
        public int TableNumber { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public double PositionX { get; set; }
        [Required]
        public double PositionY { get; set; }
    }
}
