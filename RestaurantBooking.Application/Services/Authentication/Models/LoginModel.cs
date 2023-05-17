using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Application.Services.Authentication.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Необходимо ввести emal")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = null!;
    }
}
