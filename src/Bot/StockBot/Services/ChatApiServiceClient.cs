using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Core.Services;
using StockBot.Configurations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockBot.Services
{
    public class ChatApiServiceClient : Service, IChatAPiServiceClient
    {
        private readonly IBotConfiguration _configuration;
        private HttpClient _httpClient;

        public ChatApiServiceClient(IBotConfiguration configuration)
        {
            this._configuration = configuration;
            this._httpClient = new HttpClient { BaseAddress = new Uri(configuration.ServicesUrl) };
        }

        public async Task<PostCreateResponse> SendMessage(PostCreateRequest request, UserLoginResponse userToken)
        {
            var loginContent = GetContent(request);

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken.AccessToken);

            using var response = await _httpClient.PostAsync("/api/chat/new-message", loginContent);

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
