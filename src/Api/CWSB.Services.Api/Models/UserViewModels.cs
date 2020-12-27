using System.ComponentModel.DataAnnotations;

namespace CWSB.Services.Api.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage ="Field {0} is required.")]
        [EmailAddress(ErrorMessage ="Field {0} has an invalid format.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(100,ErrorMessage = "Field {0}  must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }
        
        [Compare("Password",ErrorMessage ="Password does not match confirmation.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserLogin
    {
        [EmailAddress(ErrorMessage = "Field {0} has an invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(100, ErrorMessage = "Field {0}  must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
