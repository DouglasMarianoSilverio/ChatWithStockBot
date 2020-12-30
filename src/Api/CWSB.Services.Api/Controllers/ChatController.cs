using CWSB.Core.Models;
using CWSB.Core.User;
using CWSB.Services.Api.Models;
using CWSB.Services.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Controllers
{

    [Authorize]
    public class ChatController : MainController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IPostService _postService;

        public ChatController(IAspNetUser aspNetUser, IPostService postService)
        {
            _aspNetUser = aspNetUser;
            _postService = postService;
        }
        
        [HttpPost("new-message")]
        public async Task<IActionResult> AddMessage(PostCreateRequest message)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _aspNetUser.GetUserEmail();

            var post = new Post(message.Message, user);

            var response = _postService.PostMessage(post);

            if ( !response.Result.Succeeded)
            {
                foreach ( var error in response.Result.ResponseResult.Errors.Messages)
                {
                    AddOperationError(error);
                }
            }
            //todo - add message to RabbitMq

            return CustomResponse(response);
        }
    }
}
