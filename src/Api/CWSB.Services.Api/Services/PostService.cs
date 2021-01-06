using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Services.Api.Data;
using CWSB.Services.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;
        

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostCreateResponse> PostMessage(Post post)
        {
            try
            {
                await _postRepository.Add(post);

                return new PostCreateResponse { Succeeded = true };
            }
            catch (Exception ex)
            {
                ResponseResult result = 
                        new ResponseResult 
                        {
                            Title = "failed to save message",
                            Status = 500                            
                        };
                result.Errors.Messages.Add(ex.Message); //not god;

                return new PostCreateResponse { Succeeded = false, ResponseResult = result };
            }            
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = await _postRepository.GetLastPosts();
            return posts;
        }
    }
}
