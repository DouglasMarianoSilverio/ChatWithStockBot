using CWSB.Core.Models;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public interface IChatAPiServiceClient
    {
        Task<PostCreateResponse> SendMessage(PostCreateRequest request, UserLoginResponse userToken);
    }
}