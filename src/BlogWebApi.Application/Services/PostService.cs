using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostResponseDto>> GetAll(int skip = 0, int take = 10)
        {
            var posts = await _postRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return PostResponseMapper.Map(posts);
        }

        public Task<PostDetailsResponseDto> GetBy(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new BadRequestException( "The postId cannot be empty Guid.");

            return GetByInternal(postId);
        }

        private async Task<PostDetailsResponseDto> GetByInternal(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            return PostDetailsResponseMapper.Map(post);
        }

        public async Task<PostDetailsResponseDto> GetCommentsBy(Guid postId, int skip, int take)
        {
            if (postId == Guid.Empty)
                throw new BadRequestException( "The postId cannot be empty Guid.");

            var post = await _postRepository.GetByIdWithCommentsAsync(postId, skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return PostDetailsResponseMapper.Map(post);
        }

        public Task<PostResponseDto> Add(PostAddRequestDto post)
        {
            if (post == null)
                throw new BadRequestException("The post cannot be null.");

            if (string.IsNullOrEmpty(post.PostName) || string.IsNullOrWhiteSpace(post.PostName))
                throw new BadRequestException("The post name cannot be null or empty.");

            if (string.IsNullOrEmpty(post.Text) || string.IsNullOrWhiteSpace(post.Text))
                throw new BadRequestException("The post text cannot be null or empty.");

            if (post.BlogId == Guid.Empty)
                throw new BadRequestException("The blogId cannot be empty Guid.");

            if (post.PostName.Length > 255)
            {
                throw new BadRequestException("The post name cannot be longer than 255 characters.");
            }

            return AddInternal(post);
        }

        private async Task<PostResponseDto> AddInternal(PostAddRequestDto postAddRequestDto)
        {
            var post = PostAddRequestMapper.Map(postAddRequestDto);

            return PostResponseMapper.Map(await _postRepository.AddAsync(post));
        }

        public Task Update(PostUpdateRequestDto post)
        {
            if (post == null)
                throw new BadRequestException( "The post cannot be null.");

            if (post.PostId == Guid.Empty)
                throw new BadRequestException( "The postId cannot be empty Guid.");

            if (string.IsNullOrEmpty(post.PostName) || string.IsNullOrWhiteSpace(post.PostName))
                throw new BadRequestException( "The post name cannot be null or empty.");

            if (string.IsNullOrEmpty(post.Text) || string.IsNullOrWhiteSpace(post.Text))
                throw new BadRequestException( "The post text cannot be null or empty.");

            if (post.BlogId == Guid.Empty)
                throw new BadRequestException("The blogId cannot be empty Guid.");

            if (post.PostName.Length > 255)
            {
                throw new BadRequestException("The post name cannot be longer than 255 characters.");
            }

            return UpdateInternal(post);
        }

        private async Task UpdateInternal(PostUpdateRequestDto postUpdateRequestDto)
        {
            var oldPost = await _postRepository.GetByIdAsync(postUpdateRequestDto.PostId);

            if (oldPost == null)
            {
                throw new NotFoundException(nameof(postUpdateRequestDto.PostId), postUpdateRequestDto.PostId);
            }

            oldPost = PostUpdateRequestMapper.Map(postUpdateRequestDto);

            await _postRepository.UpdateAsync(oldPost);
        }

        public Task Delete(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new BadRequestException( "The postId cannot be empty Guid.");

            return DeleteInternal(postId);
        }

        private async Task DeleteInternal(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
            {
                throw new NotFoundException(nameof(postId), postId);
            }

            await _postRepository.DeleteAsync(post);
        }
    }
}