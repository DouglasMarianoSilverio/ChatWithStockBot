using CWSB.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public interface IUserAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLogin userLogin);
        Task<UserLoginResponse> Register(UserRegister userRegister);

    }
}
