using CWSB.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public class ChatHub : Hub,IChatHub
    {
        public async Task BroadcastMessage(Post post)
        {
             await Clients.All.SendAsync("ReceiveMessage",post.User,post.Text,post.Date);
        }
        
    }
}
