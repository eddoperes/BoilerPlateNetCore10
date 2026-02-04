using System.ComponentModel.DataAnnotations;

namespace BoilerPlateNetCore10.API.Models
{
    public class LoginModel
    {


        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max " +
            "{1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
