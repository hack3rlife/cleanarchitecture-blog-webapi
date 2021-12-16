using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System;

namespace BlogWebApi.Application.Mappers
{
    public class CommentMapper
    {
        public static Comment Map(CommentAddRequestDto commentAddRequestDto)
        {
            if (commentAddRequestDto == null)
            {
                return null;
            }

            return new Comment
            {
                PostId = commentAddRequestDto.PostId,
                CommentId = commentAddRequestDto.CommentId,
                CommentName = commentAddRequestDto.CommentName,
                Email = commentAddRequestDto.Email,
                CreatedBy = commentAddRequestDto.CreatedBy,
                CreatedDate = commentAddRequestDto.CreatedDate
            };
        }

        public static Comment Map(CommentUpdateRequestDto commentUpdateRequestDto)
        {
            if (commentUpdateRequestDto == null)
            {
                return null;
            }

            return new Comment
            {
                PostId = commentUpdateRequestDto.PostId,
                CommentId = commentUpdateRequestDto.CommentId,
                CommentName = commentUpdateRequestDto.CommentName,
                Email = commentUpdateRequestDto.Email,
                UpdatedBy = commentUpdateRequestDto.UpdatedBy,
                LastUpdate = commentUpdateRequestDto.LastUpdate
            };
        }
    }
}
