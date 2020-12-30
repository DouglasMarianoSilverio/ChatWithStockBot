using CWSB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public interface IUserChatService
    {
        Task<PostCreateResponse> SendMessage(PostCreateRequest request);
        
    }
}
