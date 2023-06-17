using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Application.Services.UserService;
using RestaurantBooking.Data;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.TableService
{
    internal class TableService : ITableService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IUserService userService;

        public TableService(ApplicationDbContext dbContext, IUserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        public void Add(int restaurantId, Table newModel)
        {
            newModel.RestaurantId = restaurantId;
            dbContext.Tables.Add(newModel);
        }

        public IQueryable<Table> GetAll()
        {
            return dbContext.Tables;
        }

        public IQueryable<TableClaim> GetAllClaims()
        {
            return dbContext.TableClaims
                .Include(t => t.Table.Restaurant)
                .Include(t => t.User)
                .AsNoTracking();
        }

        public TableClaim? GetTableClaimById(int tableClaimId)
        {
            return dbContext.TableClaims.Find(tableClaimId);
        }

        public void ClaimTable(TableClaim tableClaim, string userEmail)
        {
            var user = userService.GetByEmail(userEmail) ?? throw new InvalidOperationException("User with the given email does not exist");

            Table? table = dbContext.Tables.Find(tableClaim.TableId) ?? throw new InvalidOperationException("Table with the given id does not exist");

            dbContext.Entry(table).Collection(t => t.TableClaims).Load();

            if (table.IsClaimed)
                throw new InvalidOperationException("Table was already claimed");

            var rest = dbContext.Restaurants.Find(table.RestaurantId) ?? throw new InvalidOperationException("Table with the given id was not found");

            if (tableClaim.ClaimFromDate.TimeOfDay < rest.OpenFrom || tableClaim.ClaimToDate.TimeOfDay > rest.OpenTo)
                throw new InvalidOperationException("Time for claiming is wrong. Work time of restaurant does not fit with the claimed time");

            tableClaim.UserId = user.Id;
            tableClaim.CreatedDate = DateTime.UtcNow;

            dbContext.TableClaims.Add(tableClaim);

            dbContext.SaveChanges();
        }

        public void UnclaimTable(int tableClaimId)
        {
            var tableClaim = dbContext.TableClaims.Find(tableClaimId) ?? throw new InvalidOperationException("Table claim with the given id was not found");

            tableClaim.IsCanceled = true;

            dbContext.Update(tableClaim);
            dbContext.SaveChanges();
        }

        public void RemoveClaim(int tableClaimId)
        {
            var tableClaim = dbContext.TableClaims.Find(tableClaimId);

            if (tableClaim is null)
                throw new InvalidOperationException("Table claim was not found");

            dbContext.TableClaims.Remove(tableClaim);
            dbContext.SaveChanges();
        }
    }
}
