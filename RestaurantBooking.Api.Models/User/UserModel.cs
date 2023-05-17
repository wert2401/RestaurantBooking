using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.User
{
    public class UserModel : BaseModel
    {
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }

        public List<RoleModel> Roles { get; set; } = new();
    }
}
