using CWSB.Services.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public class PostService : IPostService
    {
        public async Task<PostCreateResponse> PostMessage(Post post)
        {
            var result = new PostCreateResponse();



            return result;
        }
    }
}
