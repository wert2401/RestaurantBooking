using RestaurantBooking.Data;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RoleService
{
    internal class RoleService : IRoleService
    {
        private readonly ApplicationDbContext dbContext;

        public RoleService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Role? GetRoleByName(string roleName)
        {
            return dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
        }
    }
}
