using CWSB.Core.Models;
using CWSB.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Controllers
{
    
    public class ChatController : MainController
    {
        private readonly IUserChatService _chatService;

        public ChatController(IUserChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("Chat")]
        public IActionResult ViewMessage()
        {
            return View();
        }

        [HttpPost]
        [Route("Chat")]
        public async Task<IActionResult> ViewMessage(PostCreateRequest postMessage)
        {
            if (!ModelState.IsValid) return View(postMessage);

            var response = await _chatService.SendMessage(postMessage);

            if (ResponseHasErrors(response.ResponseResult)) return View(postMessage);

            ModelState.Clear();

            return View(new PostCreateRequest());


        }

    }
}
