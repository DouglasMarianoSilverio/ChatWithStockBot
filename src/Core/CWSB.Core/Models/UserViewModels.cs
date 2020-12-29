using CWSB.Core.Communications;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CWSB.Core.Models
{

    public class UserRegister
    {
        [Required(ErrorMessage = "Field {0} is required.")]
        [EmailAddress(ErrorMessage = "Field {0} has an invalid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required.")]
        [StringLength(100, ErrorMessage = "Field {0}  must be between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "Password does not match confirmation.")]
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

    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

}
