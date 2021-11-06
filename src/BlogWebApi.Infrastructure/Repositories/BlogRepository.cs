using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class BlogRepository : EfRepository<Blog>, IBlogRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public BlogRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public async Task<Blog> GetByIdWithPostsAsync(Guid blogId, int skip = 0, int take = 10)
        {
            return await _blogDbContext
                .Blog
                .Where(blog => blog.BlogId == blogId)
                .Include(b=> 
                    b.Post
                        .OrderByDescending(p=> p.CreatedDate)
                        .Skip(skip)
                        .Take(take))
                .FirstOrDefaultAsync();
        }
    }
}
