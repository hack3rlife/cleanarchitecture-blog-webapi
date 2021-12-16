using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<IEnumerable<CommentResponseDto>> GetAll(int skip = 0, int take = 10)
        {
            var comments = await _commentRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return CommentResponseMapper.Map(comments);
        }

        public Task<CommentDetailsResponseDto> GetBy(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentId), "The commentId cannot be empty Guid.");

            return GetByInternal(commentId);
        }

        private async Task<CommentDetailsResponseDto> GetByInternal(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            return CommentDetailsResponseMapper.Map(comment);
        }

        public Task<CommentResponseDto> Add(CommentAddRequestDto commentAddRequestDto)
        {
            if (commentAddRequestDto == null)
                throw new ArgumentNullException(nameof(commentAddRequestDto), "The comment cannot be null.");

            if (string.IsNullOrEmpty(commentAddRequestDto.CommentName) || string.IsNullOrWhiteSpace(commentAddRequestDto.CommentName))
                throw new ArgumentNullException(nameof(commentAddRequestDto), "The comment name cannot be null or empty.");
            
            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(commentAddRequestDto.Email) || string.IsNullOrWhiteSpace(commentAddRequestDto.Email))
                throw new ArgumentNullException(nameof(commentAddRequestDto), "The email cannot be null or empty.");
            
            if (commentAddRequestDto.PostId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentAddRequestDto), "The postId cannot be empty Guid.");

            return AddInternal(commentAddRequestDto);
        }

        private async Task<CommentResponseDto> AddInternal(CommentAddRequestDto commentAddRequestDto)
        {
            var newComment = await _commentRepository.AddAsync(CommentMapper.Map(commentAddRequestDto));

            return CommentResponseMapper.Map(newComment);
        }

        public Task Update(CommentUpdateRequestDto commentUpdateRequestDto)
        {
            if(commentUpdateRequestDto == null)
                throw new ArgumentNullException(nameof(commentUpdateRequestDto), "The comment cannot be null.");

            if (commentUpdateRequestDto.CommentId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentUpdateRequestDto), "The commentId cannot be empty Guid.");

            if (string.IsNullOrEmpty(commentUpdateRequestDto.CommentName) || string.IsNullOrWhiteSpace(commentUpdateRequestDto.CommentName))
                throw new ArgumentNullException(nameof(commentUpdateRequestDto), "The comment name cannot be null or empty.");

            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(commentUpdateRequestDto.Email) || string.IsNullOrWhiteSpace(commentUpdateRequestDto.Email))
                throw new ArgumentNullException(nameof(commentUpdateRequestDto), "The email cannot be null or empty.");

            if (commentUpdateRequestDto.PostId == Guid.Empty)
                throw new ArgumentNullException(nameof(commentUpdateRequestDto), "The postId cannot be empty Guid.");

            return UpdateInternal(commentUpdateRequestDto);
        }

        private async Task UpdateInternal(CommentUpdateRequestDto comment)
        {
            var oldComment = await _commentRepository.GetByIdAsync(comment.CommentId);

            if (oldComment == null)
            {
                throw new ArgumentNullException(nameof(comment.CommentId), $"The comment with {comment.CommentId} does not exist.");
            }

            oldComment = CommentMapper.Map(comment);

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

            if (comment == null)
            {
                throw new ArgumentNullException(nameof(commentId), $"The comment with {commentId} does not exist.");
            }

            await _commentRepository.DeleteAsync(comment);
        }
    }
}
