using System;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class PostRepository : EfRepository<Post>, IPostRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public PostRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<Post> GetByIdWithCommentsAsync(Guid postId, int skip = 0, int take = 10)
        {
            return await _blogDbContext
                .Post
                .Where(p => p.PostId == postId)
                .Include(p => 
                    p.Comment
                        .Where(c=> c.PostId == postId)
                        .OrderByDescending(c=> c.CreatedDate)
                        .Skip(skip)
                        .Take(take))
                .FirstOrDefaultAsync();
        }
    }
}