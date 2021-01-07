using CWSB.Core.Models;
using StockBot.Configurations;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public interface IApiAuthenticationService
    {
        Task<UserLoginResponse> Login();

    }
}
