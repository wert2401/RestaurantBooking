namespace RestaurantBooking.Api.Models.Table
{
    public class TableModel : BaseModel
    {
        public int TableNumber { get; set; }
        public int RestaurantId { get; set; }
        public string? Restaurant { get; set; } = null;
        public bool IsClaimed { get; set; }
        public DateTime VacantFrom { get; set; }
    }
}