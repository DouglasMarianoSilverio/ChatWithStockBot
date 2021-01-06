using CWSB.Core.Models;
using CWSB.Services.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWSB.Services.Api.Repository
{
    public class PostRepository : IPostRepository
    {

        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Post> GetPosts()
        {
            return _context.Posts.AsQueryable();
        }

        public async Task<List<Post>> GetLastPosts()
        {
            var result = await _context.Posts.OrderByDescending(p => p.Date).Take(50).ToListAsync();
            return result;
        }



    }
}
