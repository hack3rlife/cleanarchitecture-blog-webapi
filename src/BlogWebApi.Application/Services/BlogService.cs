using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> GetAll(int skip = 0, int take = 10)
        {
            return await _blogRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        public Task<Blog> GetBy(Guid blogId)
        {
            if (blogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid.");
           
            return GetByInternal(blogId);
        }

        private async Task<Blog> GetByInternal(Guid blogId)
        {
            return await _blogRepository.GetByIdAsync(blogId);
        }

        public Task<Blog> GetPostsBy(Guid blogId, int skip = 0, int take = 10)
        {
            if (blogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid.");

            return GetPostsByInternal(blogId, skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        private async Task<Blog> GetPostsByInternal(Guid blogId, int skip, int take)
        {
            return await _blogRepository.GetByIdWithPostsAsync(blogId, skip, take);
        }

        public Task<Blog> Add(Blog blog)
        {
            if (blog == null)
                throw new ArgumentNullException(nameof(blog));

            if (string.IsNullOrEmpty(blog.BlogName) || string.IsNullOrWhiteSpace(blog.BlogName))
                throw new ArgumentNullException(nameof(blog), "The blog name cannot be null or empty.");

            if (blog.BlogName.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blog.BlogName), "The blog name cannot be longer than 255 characters.");
            }

            return AddInternal(blog);
        }

        public async Task<Blog> AddInternal(Blog blog)
        {
            return await _blogRepository.AddAsync(blog);
        }

        public Task Update(Blog blog)
        {
            if (blog == null)
                throw new ArgumentNullException(nameof(blog));

            if (blog.BlogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blog), "The blogId cannot be empty Guid.");

            if (string.IsNullOrEmpty(blog.BlogName) || string.IsNullOrWhiteSpace(blog.BlogName))
                throw new ArgumentNullException(nameof(blog), "The blog name cannot be null or empty.");

            if (blog.BlogName.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blog.BlogName), "The blog name cannot be longer than 255 characters.");
            }

            return UpdateInternal(blog);
        }

        private async Task UpdateInternal(Blog blog)
        {
            var oldBlog = await _blogRepository.GetByIdAsync(blog.BlogId);

            if (oldBlog == null)
            {
                throw new ArgumentException($"The blog with {blog.BlogId} does not exist.", nameof(blog.BlogId));
            }

            oldBlog.BlogName = blog.BlogName;
            oldBlog.UpdatedBy = blog.UpdatedBy;

            await _blogRepository.UpdateAsync(oldBlog);
        }

        public Task Delete(Guid blogId)
        {
            if (blogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid");

            return DeleteInternal(blogId);
        }

        private async Task DeleteInternal(Guid blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);

            if (blog == null)
            {
                throw new ArgumentException($"The blogId {blogId} does not exist.", nameof(blogId));
            }

            await _blogRepository.DeleteAsync(blog);
        }
    }
}
