using AutoMapper;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CommentResponseDto>> GetAll(int skip, int take)
        {
            var comments = await _commentRepository.ListAllAsync(skip < 0 ? 0 : skip, take <= 0 ? 10 : take);

            return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
        }

        public Task<CommentDetailsResponseDto> GetBy(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new BadRequestException( "The commentId cannot be empty Guid.");

            return GetByInternal(commentId);
        }

        private async Task<CommentDetailsResponseDto> GetByInternal(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            return _mapper.Map<CommentDetailsResponseDto>(comment);
        }

        public Task<CommentResponseDto> Add(CommentAddRequestDto commentAddRequestDto)
        {
            if (commentAddRequestDto == null)
                throw new BadRequestException("The comment cannot be null.");

            if (string.IsNullOrEmpty(commentAddRequestDto.CommentName) || string.IsNullOrWhiteSpace(commentAddRequestDto.CommentName))
                throw new BadRequestException("The comment name cannot be null or empty.");
            
            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(commentAddRequestDto.Email) || string.IsNullOrWhiteSpace(commentAddRequestDto.Email))
                throw new BadRequestException("The email cannot be null or empty.");
            
            if (commentAddRequestDto.PostId == Guid.Empty)
                throw new BadRequestException("The postId cannot be empty Guid.");

            return AddInternal(commentAddRequestDto);
        }

        private async Task<CommentResponseDto> AddInternal(CommentAddRequestDto commentAddRequestDto)
        {
            var newComment = await _commentRepository.AddAsync(_mapper.Map<Comment>(commentAddRequestDto));

            return _mapper.Map<CommentResponseDto>(newComment);
        }

        public Task Update(CommentUpdateRequestDto commentUpdateRequestDto)
        {
            if(commentUpdateRequestDto == null)
                throw new BadRequestException("The comment cannot be null.");

            if (commentUpdateRequestDto.CommentId == Guid.Empty)
                throw new BadRequestException("The commentId cannot be empty Guid.");

            if (string.IsNullOrEmpty(commentUpdateRequestDto.CommentName) || string.IsNullOrWhiteSpace(commentUpdateRequestDto.CommentName))
                throw new BadRequestException("The comment name cannot be null or empty.");

            //TODO: Validate email is valid
            if (string.IsNullOrEmpty(commentUpdateRequestDto.Email) || string.IsNullOrWhiteSpace(commentUpdateRequestDto.Email))
                throw new BadRequestException("The email cannot be null or empty.");

            if (commentUpdateRequestDto.PostId == Guid.Empty)
                throw new BadRequestException("The postId cannot be empty Guid.");

            return UpdateInternal(commentUpdateRequestDto);
        }

        private async Task UpdateInternal(CommentUpdateRequestDto comment)
        {
            var oldComment = await _commentRepository.GetByIdAsync(comment.CommentId);

            if (oldComment == null)
            {
                throw new NotFoundException(nameof(comment), comment.CommentId);
            }

            oldComment = _mapper.Map<Comment>(comment);

            await _commentRepository.UpdateAsync(oldComment);
        }

        public Task Delete(Guid commentId)
        {
            if(commentId == Guid.Empty)
                throw new BadRequestException( "The commentId cannot be empty Guid.");

            return DeleteInternal(commentId);
        }

        private async Task DeleteInternal(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
            {
                throw new NotFoundException(nameof(comment), commentId);
            }

            await _commentRepository.DeleteAsync(comment);
        }
    }
}
