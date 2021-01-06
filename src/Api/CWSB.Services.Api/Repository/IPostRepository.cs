using CWSB.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Repository
{
    public interface IPostRepository
    {
        Task Add(Post post);
        IQueryable<Post> GetPosts();
        Task<List<Post>> GetLastPosts();

    }
}
