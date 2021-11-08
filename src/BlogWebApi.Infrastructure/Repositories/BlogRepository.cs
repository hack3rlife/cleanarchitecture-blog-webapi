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
            // TODO: Fix this. Not the best approach but at least is working.
            //return await _blogDbContext
            //    .Blog
            //    .Where(blog => blog.BlogId == blogId)
            //    .Include(b => b.Post
            //            .Where(post => post.BlogId == blogId)
            //            .OrderByDescending(p => p.CreatedDate)
            //            .Skip(skip)
            //            .Take(take).ToHashSet())
            //    .FirstOrDefaultAsync();

            var blog = await _blogDbContext.Blog.FirstOrDefaultAsync(b => b.BlogId == blogId);

            if (blog == null)
            {
                return null;
            }

            var posts = blog?.Post?.Skip(skip).Take(take).ToList();

            return new Blog
            {
                BlogId = blog.BlogId,
                BlogName = blog.BlogName,
                CreatedDate = blog.CreatedDate,
                CreatedBy = blog.CreatedBy,
                LastUpdate = blog.LastUpdate,
                UpdatedBy = blog.UpdatedBy,
                Post = posts
            };

        }
    }
}