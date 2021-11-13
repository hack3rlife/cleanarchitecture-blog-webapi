using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Mappers;
using BlogWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<BlogDetailsResponseDto>> GetAll(int skip = 0, int take = 10)
        {
            var blogs = await _blogRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return BlogMapper.ToBlogCollectionResponse(blogs);
            
        }

        public Task<BlogByIdResponseDto> GetBy(Guid blogId)
        {
            if (blogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid.");
           
            return  GetByInternal(blogId);
        }

        private async Task<BlogByIdResponseDto> GetByInternal(Guid blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);

            return BlogMapper.ToBlogByIdResponseDto(blog);
        }

        public Task<BlogDetailsResponseDto> GetPostsBy(Guid blogId, int skip = 0, int take = 10)
        {
            if (blogId == Guid.Empty)
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid.");

            return GetPostsByInternal(blogId, skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        private async Task<BlogDetailsResponseDto> GetPostsByInternal(Guid blogId, int skip, int take)
        {
            var blog = await _blogRepository.GetByIdWithPostsAsync(blogId, skip, take);

            return BlogMapper.ToBlogResponse(blog);
        }

        public Task<BlogDetailsResponseDto> Add(BlogAddRequestDto blogAddRequestDto)
        {
            if (blogAddRequestDto == null)
                throw new ArgumentNullException(nameof(blogAddRequestDto));

            if (string.IsNullOrEmpty(blogAddRequestDto.BlogName) ||
                string.IsNullOrWhiteSpace(blogAddRequestDto.BlogName))
                throw new ArgumentNullException(nameof(blogAddRequestDto), "The blog name cannot be null or empty.");

            if (blogAddRequestDto.BlogName.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(blogAddRequestDto.BlogName),
                    "The blog name cannot be longer than 255 characters.");
            }

            var blog = BlogMapper.FromBlogAddRequestDto(blogAddRequestDto);

            return AddInternal(blog);
        }

        public async Task<BlogDetailsResponseDto> AddInternal(Blog blog)
        {
            var newPost = await _blogRepository.AddAsync(blog);

            return BlogMapper.ToBlogResponse(newPost);
        }

        public Task Update(BlogUpdateRequestDto blog)
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

        private async Task UpdateInternal(BlogUpdateRequestDto blog)
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
                throw new ArgumentNullException(nameof(blogId), "The blogId cannot be empty Guid.");

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
