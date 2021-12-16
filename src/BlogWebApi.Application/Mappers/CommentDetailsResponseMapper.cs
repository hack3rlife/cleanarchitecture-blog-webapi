using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System;

namespace BlogWebApi.Application.Mappers
{
    public class CommentDetailsResponseMapper
    {
        public static CommentDetailsResponseDto Map(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new CommentDetailsResponseDto
            {
                CommentId = comment.CommentId,
                CommentName = comment.CommentName,
                PostId = comment.PostId,
                CreatedBy = comment.CreatedBy,
                CreatedDate = comment.CreatedDate,
                UpdatedBy = comment.UpdatedBy,
                LastUpdate = comment.LastUpdate,
                Email = comment.Email
            };
        }
    }
}
