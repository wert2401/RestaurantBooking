namespace RestaurantBooking.Data.Entities
{
    public class TableClaim : BaseEntity
    {
        public int TableId { get; set; }
        public int UserId { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ClaimFromDate { get; set; }
        public DateTime ClaimToDate { get; set; }
        public virtual Table Table { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
