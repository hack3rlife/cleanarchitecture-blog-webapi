﻿using BlogWebApi.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDto>> GetAll(int skip = 0, int take = 10);
        Task<CommentDetailsResponseDto> GetBy(Guid commentId);
        Task<CommentResponseDto> Add(CommentAddRequestDto commentAddRequestDto);
        Task Update(CommentUpdateRequestDto commentUpdateRequestDto);
        Task Delete(Guid commentId);
    }
}