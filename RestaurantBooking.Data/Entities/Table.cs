using Microsoft.EntityFrameworkCore;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(TableNumber), nameof(RestaurantId), IsUnique = true)]
    public class Table : BaseEntity
    {
        public int TableNumber { get; set; }
        public int RestaurantId { get; set; }

        public bool IsClaimed => TableClaims.Any(c => !c.IsExpired && !c.IsCanceled || TableClaims.Count == 0);

        public DateTime VacantFrom => TableClaims.Select(c => c.ClaimToDate).OrderByDescending(c => c).FirstOrDefault();

        public Restaurant Restaurant { get; set; } = null!;

        public List<TableClaim> TableClaims { get; set; } = new();
    }
}
