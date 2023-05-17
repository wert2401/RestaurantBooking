using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Application.Services.RoleService
{
    public interface IRoleService
    {
        Role GetRoleByName(string roleName);
    }
}
