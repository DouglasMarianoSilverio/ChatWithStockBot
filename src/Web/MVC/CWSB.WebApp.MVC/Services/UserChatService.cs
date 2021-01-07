using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using CWSB.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CWSB.WebApp.MVC.Services
{
    public class UserChatService : Service, IUserChatService

    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

        public UserChatService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            httpClient.BaseAddress = new Uri(_settings.ServicesUrl);
        }

        public string GetServicesUrl()
        {
            return _settings.ServicesUrl;
        }

        public async Task<PostCreateResponse> SendMessage(PostCreateRequest request)
        {
            var messageContent = GetContent(request);
            var response = await _httpClient.PostAsync("/api/chat/new-message", messageContent);

            if (!HandleErrorsResponse(response))
            {
                return new PostCreateResponse
                {
                    ResponseResult = await DeserializeResponseObject<ResponseResult>(response)
                };
            }

            return await DeserializeResponseObject<PostCreateResponse>(response);
        }
    }
}
