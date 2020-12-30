using CWSB.Core.Communications;
using CWSB.Core.Models;
using CWSB.Services.Api.Data;
using CWSB.Services.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Services
{
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PostCreateResponse> PostMessage(Post post)
        {
            try
            {
                await _context.AddAsync(post);

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
    }
}
