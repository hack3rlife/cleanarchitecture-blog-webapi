using AutoMapper;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogResponseDto>> GetAll(int skip = 0, int take = 10)
        {
            var blogs = await _blogRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return _mapper.Map<IEnumerable<BlogResponseDto>>(blogs);
        }

        public Task<BlogResponseDto> GetBy(Guid blogId)
        {
            if (blogId == Guid.Empty)
                throw new BadRequestException( $"The {nameof(blogId)} cannot be empty Guid.");
           
            return  GetByInternal(blogId);
        }

        private async Task<BlogResponseDto> GetByInternal(Guid blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);

            return _mapper.Map<BlogResponseDto>(blog);
        }

        public Task<BlogDetailsResponseDto> GetPostsBy(Guid blogId, int skip = 0, int take = 10)
        {
            if (blogId == Guid.Empty)
                throw new BadRequestException($"The {nameof(blogId)} cannot be empty Guid.");

            return GetPostsByInternal(blogId, skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        private async Task<BlogDetailsResponseDto> GetPostsByInternal(Guid blogId, int skip, int take)
        {
            var blog = await _blogRepository.GetByIdWithPostsAsync(blogId, skip, take);

            return _mapper.Map<BlogDetailsResponseDto>(blog);
        }

        public Task<BlogResponseDto> Add(BlogAddRequestDto blogAddRequestDto)
        {
            if (blogAddRequestDto == null)
                throw new BadRequestException("Blog information cannot be null.");

            if (string.IsNullOrEmpty(blogAddRequestDto.BlogName) ||
                string.IsNullOrWhiteSpace(blogAddRequestDto.BlogName))
                throw new BadRequestException( "The blog name cannot be null or empty.");

            if (blogAddRequestDto.BlogName.Length > 255)
            {
                throw new BadRequestException("The blog name cannot be longer than 255 characters.");
            }

            var blog = _mapper.Map<Blog>(blogAddRequestDto);

            return AddInternal(blog);
        }

        public async Task<BlogResponseDto> AddInternal(Blog blog)
        {
            var newPost = await _blogRepository.AddAsync(blog);

            return _mapper.Map<BlogResponseDto>(newPost);
        }

        public Task Update(BlogUpdateRequestDto blog)
        {
            if (blog == null)
                throw new BadRequestException("The blog information cannot be null.");

            if (blog.BlogId == Guid.Empty)
                throw new BadRequestException("The blogId cannot be empty Guid.");

            if (string.IsNullOrEmpty(blog.BlogName) || string.IsNullOrWhiteSpace(blog.BlogName))
                throw new BadRequestException( "The blog name cannot be null or empty.");

            if (blog.BlogName.Length > 255)
            {
                throw new BadRequestException("The blog name cannot be longer than 255 characters.");
            }

            return UpdateInternal(blog);
        }

        private async Task UpdateInternal(BlogUpdateRequestDto blog)
        {
            var oldBlog = await _blogRepository.GetByIdAsync(blog.BlogId);

            if (oldBlog == null)
            {
                throw new NotFoundException(nameof(blog), blog.BlogId);
            }

            oldBlog.BlogName = blog.BlogName;
            oldBlog.UpdatedBy = blog.UpdatedBy;

            await _blogRepository.UpdateAsync(oldBlog);
        }

        public Task Delete(Guid blogId)
        {
            if (blogId == Guid.Empty)
                throw new BadRequestException($"The {nameof(blogId)} cannot be empty Guid.");

            return DeleteInternal(blogId);
        }

        private async Task DeleteInternal(Guid blogId)
        {
            var blog = await _blogRepository.GetByIdAsync(blogId);

            if (blog == null)
            {
                throw new NotFoundException(nameof(blogId), blogId);
            }

            await _blogRepository.DeleteAsync(blog);
        }
    }
}
