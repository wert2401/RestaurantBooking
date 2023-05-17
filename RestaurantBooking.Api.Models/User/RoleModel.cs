namespace RestaurantBooking.Api.Models.User
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
