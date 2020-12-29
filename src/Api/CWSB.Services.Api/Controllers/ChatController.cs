using CWSB.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Controllers
{
    
    [Authorize]
    public class ChatController : MainController
    {
        private readonly IAspNetUser aspNetUser;

        public ChatController(IAspNetUser aspNetUser)
        {
            this.aspNetUser = aspNetUser;
        }

        public IActionResult AddMessage(string message)
        {
            return View();
        }
    }
}
