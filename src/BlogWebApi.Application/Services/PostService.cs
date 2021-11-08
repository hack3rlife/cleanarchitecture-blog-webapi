using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAll(int skip = 0, int take = 10)
        {
            return await _postRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        public Task<Post> GetBy(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId), "The postId cannot be empty Guid.");

            return GetByInternal(postId);
        }

        private async Task<Post> GetByInternal(Guid postId)
        {
            return await _postRepository.GetByIdAsync(postId);
        }

        public async Task<Post> GetCommentsBy(Guid postId, int skip, int take)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId), "The postId cannot be empty Guid.");

            return await _postRepository.GetByIdWithCommentsAsync(postId, skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        public Task<Post> Add(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post), "The post cannot be null.");

            if (string.IsNullOrEmpty(post.PostName) || string.IsNullOrWhiteSpace(post.PostName))
                throw new ArgumentNullException(nameof(post.PostName), "The post name cannot be null or empty.");

            if (string.IsNullOrEmpty(post.Text) || string.IsNullOrWhiteSpace(post.Text))
                throw new ArgumentNullException(nameof(post.Text), "The post text cannot be null or empty.");

            if (post.BlogId == Guid.Empty)
                throw new ArgumentNullException(nameof(post.BlogId), "The blogId cannot be empty Guid.");

            if (post.PostName.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(post.PostName),
                    "The post name cannot be longer than 255 characters.");
            }

            return AddInternal(post);
        }

        private async Task<Post> AddInternal(Post post)
        {
            return await _postRepository.AddAsync(post);
        }

        public Task Update(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post), "The post cannot be null.");

            if (post.PostId == Guid.Empty)
                throw new ArgumentNullException(nameof(post), "The postId cannot be empty Guid.");

            if (string.IsNullOrEmpty(post.PostName) || string.IsNullOrWhiteSpace(post.PostName))
                throw new ArgumentNullException(nameof(post), "The post name cannot be null or empty.");

            if (string.IsNullOrEmpty(post.Text) || string.IsNullOrWhiteSpace(post.Text))
                throw new ArgumentNullException(nameof(post), "The post text cannot be null or empty.");

            if (post.BlogId == Guid.Empty)
                throw new ArgumentNullException(nameof(post), "The blogId cannot be empty Guid.");

            if (post.PostName.Length > 255)
            {
                throw new ArgumentOutOfRangeException(nameof(post.PostName),
                    "The post name cannot be longer than 255 characters.");
            }

            return UpdateInternal(post);
        }

        private async Task UpdateInternal(Post post)
        {
            var oldPost = await _postRepository.GetByIdAsync(post.PostId);

            if (oldPost == null)
            {
                throw new ArgumentException($"The post with {post.PostId} does not exist.", nameof(post.PostId));
            }

            oldPost.PostName = post.PostName;
            oldPost.Text = post.Text;
            oldPost.UpdatedBy = post.UpdatedBy;

            await _postRepository.UpdateAsync(oldPost);
        }

        public Task Delete(Guid postId)
        {
            if (postId == Guid.Empty)
                throw new ArgumentNullException(nameof(postId), "The postId cannot be empty Guid.");

            return DeleteInternal(postId);
        }

        private async Task DeleteInternal(Guid postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null)
            {
                throw new ArgumentException($"The postId {postId} does not exist.", nameof(postId));
            }

            await _postRepository.DeleteAsync(post);
        }
    }
}