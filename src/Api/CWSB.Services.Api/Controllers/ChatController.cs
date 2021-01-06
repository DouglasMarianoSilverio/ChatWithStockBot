using CWSB.Core.Extensions;
using CWSB.Core.Models;
using CWSB.Core.RabbitMQ;
using CWSB.Core.User;
using CWSB.Services.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Controllers
{

    [Authorize]
    [Route("api/chat")]
    public class ChatController : MainController
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly IPostService _postService;
        private readonly IProducerService _producerService;
        private readonly IHubContext<ChatHub> _hub;

        public ChatController(IAspNetUser aspNetUser, IPostService postService, IProducerService producerService, IHubContext<ChatHub> hub)
        {
            _aspNetUser = aspNetUser;
            _postService = postService;
            _producerService = producerService;
            _hub = hub;
        }

        [HttpPost("new-message")]        
        public async Task<IActionResult> AddMessage(PostCreateRequest message)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = new PostCreateResponse { Succeeded = true };

            var user = _aspNetUser.GetUserEmail();

            var post = new Post(message.Message, user);

            if (!post.IsCommand())
            {

                var response = await _postService.PostMessage(post);

                if (!response.Succeeded)
                {
                    foreach (var error in response.ResponseResult.Errors.Messages)
                    {
                        AddOperationError(error);
                    }
                }
                result = response;
            }
            

            await _hub.Clients.All.SendAsync("ReceiveMessage",post.User,post.Text,post.Date);

            if (post.IsCommand()) {  _producerService.Produce(post).ConfigureAwait(false) ; }

            return CustomResponse(result);
        }
    }
}
