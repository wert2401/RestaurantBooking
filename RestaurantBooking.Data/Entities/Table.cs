using EntityFrameworkCore.Projectables;
using Microsoft.EntityFrameworkCore;

namespace RestaurantBooking.Data.Entities
{
    [Index(nameof(TableNumber), nameof(RestaurantId), IsUnique = true)]
    public class Table : BaseEntity
    {
        public int TableNumber { get; set; }
        public int RestaurantId { get; set; }

        [Projectable] public bool IsClaimed => TableClaims.Any(c => c.ClaimToDate > DateTime.UtcNow && !c.IsCanceled || TableClaims.Count == 0);

        [Projectable] public DateTime VacantFrom => TableClaims.Where(t => !t.IsCanceled).Select(c => c.ClaimToDate).OrderByDescending(c => c).FirstOrDefault();
        public virtual Restaurant Restaurant { get; set; } = null!;

        public virtual ICollection<TableClaim> TableClaims { get; set; }
    }
}
