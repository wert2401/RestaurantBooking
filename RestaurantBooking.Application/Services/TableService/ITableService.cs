using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.TableService
{
    public interface ITableService
    {
        void Add(int restaurantId, Table newModel);
        void ClaimTable(int tableId, string userEmail, DateTime claimDate);
        void UnclaimTable(int tableClaimId);
        IQueryable<Table> GetAll();
        IQueryable<TableClaim> GetAllClaims();
        TableClaim? GetTableClaimById(int tableClaimId);
    }
}