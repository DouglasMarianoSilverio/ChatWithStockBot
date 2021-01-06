using CWSB.Core.Models;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public interface IApiAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLogin userLogin);

    }
}
