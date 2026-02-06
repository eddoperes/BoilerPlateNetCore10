using System.ComponentModel.DataAnnotations;

namespace BoilerPlateNetCore10.API.Models
{
    public class RevokeTokenModel
    {

        [Required(ErrorMessage = "User id is required")]
        public long UserId { get; set; }

    }
}
