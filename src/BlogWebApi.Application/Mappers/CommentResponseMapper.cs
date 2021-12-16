using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BlogWebApi.Application.Mappers
{
    public class CommentResponseMapper
    {
        public static IEnumerable<CommentResponseDto> Map(IReadOnlyList<Comment> comments)
        {
            if (comments == null)
            {
                return null;
            }

            return comments.Select(comment => 
                new CommentResponseDto
                {
                    CommentId = comment.CommentId,
                    CommentName = comment.CommentName,
                    PostId = comment.PostId,

                }).ToList();
        }

        public static CommentResponseDto Map(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            return new CommentResponseDto
            {
                CommentId = comment.CommentId,
                CommentName = comment.CommentName,
                PostId = comment.PostId,
            };
        }
    }
}
