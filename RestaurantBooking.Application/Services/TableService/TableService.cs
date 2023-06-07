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
            return dbContext.Tables.AsNoTracking().Include(t => t.TableClaims);
        }

        public IQueryable<TableClaim> GetAllClaims()
        {
            return dbContext.TableClaims.AsNoTracking();
        }

        public TableClaim? GetTableClaimById(int tableClaimId)
        {
            return dbContext.TableClaims.Find(tableClaimId);
        }

        public void ClaimTable(TableClaim tableClaim, string userEmail)
        {
            var user = userService.GetByEmail(userEmail);

            if (user == null) 
                throw new InvalidOperationException("User with the given email does not exist");

            Table? table = dbContext.Tables.Find(tableClaim.TableId);

            if (table == null) 
                throw new InvalidOperationException("Table with the given id does not exist");

            dbContext.Entry(table).Collection(t => t.TableClaims).Load();

            var hasClaimedBefore = table.TableClaims.Where(x => x.UserId == user.Id && !x.IsCanceled).ToList().Any(c => !c.IsExpired);

            if (hasClaimedBefore)
                throw new InvalidOperationException("User has claimed this table before");

            var rest = dbContext.Restaurants.Find(table.RestaurantId);

            if (rest == null)
                throw new InvalidOperationException("Table with the given id was not found");

            if (tableClaim.ClaimFromDate.TimeOfDay < rest.OpenFrom || tableClaim.ClaimToDate.TimeOfDay > rest.OpenTo)
                throw new InvalidOperationException("Time for claiming is wrong. Work time of restaurant does not fit with the claimed time");

            tableClaim.UserId = user.Id;
            tableClaim.CreatedDate = DateTime.UtcNow;

            dbContext.TableClaims.Add(tableClaim);

            dbContext.SaveChanges();
        }

        public void UnclaimTable(int tableClaimId)
        {
            var tableClaim = dbContext.TableClaims.Find(tableClaimId);

            if (tableClaim == null)
                throw new InvalidOperationException("Table claim with the given id was not found");

            tableClaim.IsCanceled = true;

            dbContext.Update(tableClaim);
            dbContext.SaveChanges();
        }
    }
}
