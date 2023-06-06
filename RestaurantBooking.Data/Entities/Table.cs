using Microsoft.EntityFrameworkCore;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(TableNumber), nameof(RestaurantId), IsUnique = true)]
    public class Table : BaseEntity
    {
        public int TableNumber { get; set; }
        public int RestaurantId { get; set; }
        public int Capacity { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }

        public bool IsClaimed => TableClaims.Any(c => !c.IsExpired || TableClaims.Count == 0);

        public Restaurant Restaurant { get; set; } = null!;

        public List<TableClaim> TableClaims { get; set; } = new();
    }
}
