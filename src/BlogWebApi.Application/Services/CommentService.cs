using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<IEnumerable<Comment>> GetAll(int skip, int take)
        {
            return await _commentRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);
        }

        public Task<Comment> GetBy(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentId), "The commentId cannot be empty Guid.");

            return GetByInternal(commentId);
        }

        private async Task<Comment> GetByInternal(Guid commentId)
        {
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public Task<Comment> Add(Comment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "The comment cannot be null.");

            if (string.IsNullOrEmpty(comment.CommentName) || string.IsNullOrWhiteSpace(comment.CommentName))
                throw new ArgumentNullException(nameof(comment), "The comment name cannot be null or empty.");
            
            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(comment.Email) || string.IsNullOrWhiteSpace(comment.Email))
                throw new ArgumentNullException(nameof(comment), "The email cannot be null or empty.");
            
            if (comment.PostId == Guid.Empty)
                throw new ArgumentNullException(nameof(comment), "The postId cannot be empty Guid.");

            return AddInternal(comment);
        }

        private async Task<Comment> AddInternal(Comment comment)
        {
            return await _commentRepository.AddAsync(comment);
        }

        public Task Update(Comment comment)
        {
            if(comment == null)
                throw new ArgumentNullException(nameof(comment), "The comment cannot be null.");

            if (comment.CommentId == Guid.Empty)
                throw new ArgumentNullException(nameof(comment), "The commentId cannot be empty Guid.");

            if (string.IsNullOrEmpty(comment.CommentName) || string.IsNullOrWhiteSpace(comment.CommentName))
                throw new ArgumentNullException(nameof(comment), "The comment name cannot be null or empty.");

            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(comment.Email) || string.IsNullOrWhiteSpace(comment.Email))
                throw new ArgumentNullException(nameof(comment), "The email cannot be null or empty.");

            if (comment.PostId == Guid.Empty)
                throw new ArgumentNullException(nameof(comment), "The postId cannot be empty Guid.");

            return UpdateInternal(comment);
        }

        private async Task UpdateInternal(Comment comment)
        {
            var oldComment = await _commentRepository.GetByIdAsync(comment.CommentId);

            if (oldComment == null)
            {
                throw new ArgumentException($"The comment with {comment.CommentId} does not exist.", nameof(comment.CommentId));
            }

            oldComment.CommentName = comment.CommentName;
            oldComment.Email = comment.Email;
            oldComment.UpdatedBy = comment.UpdatedBy;

            await _commentRepository.UpdateAsync(oldComment);
        }

        public Task Delete(Guid commentId)
        {
            if(commentId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentId), "The commentId cannot be empty Guid.");

            return DeleteInternal(commentId);
        }

        private async Task DeleteInternal(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            await _commentRepository.DeleteAsync(comment);
        }
    }
}
