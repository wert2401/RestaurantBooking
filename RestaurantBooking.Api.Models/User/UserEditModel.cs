using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Api.Models.User
{
    public class UserEditModel
    {
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
    }
}
