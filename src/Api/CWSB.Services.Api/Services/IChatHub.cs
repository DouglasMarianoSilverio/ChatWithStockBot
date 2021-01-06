using CWSB.Core.Models;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public interface IChatHub
    {
        Task BroadcastMessage(Post post);
    }
}