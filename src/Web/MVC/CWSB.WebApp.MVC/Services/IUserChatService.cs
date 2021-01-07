using CWSB.Core.Models;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public interface IUserChatService
    {
        string GetServicesUrl();
        Task<PostCreateResponse> SendMessage(PostCreateRequest request);
        
    }
}
