using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.TableService
{
    public interface ITableService
    {
        void Add(Table newModel);
        void ClaimTable(int tableId, string userEmail, DateTime claimDate);
        IQueryable<Table> GetAll();
    }
}