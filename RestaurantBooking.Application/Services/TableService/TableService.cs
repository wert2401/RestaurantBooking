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

        public void ClaimTable(int tableId, string userEmail, DateTime claimDate)
        {
            var user = userService.GetByEmail(userEmail);

            if (user == null) throw new InvalidOperationException("User with the given email does not exist");

            Table? table = dbContext.Tables.Find(tableId);

            if (table == null) throw new InvalidOperationException("Table with the given id does not exist");

            dbContext.TableClaims.Add(new TableClaim
            {
                UserId = user.Id,
                TableId = tableId,
                ClaimDate = claimDate,
                CreatedDate = DateTime.Now
            });

            dbContext.Tables.Update(table);

            dbContext.SaveChanges();
        }
    }
}
