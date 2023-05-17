using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.User
{
    public class UserEditModel
    {
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
