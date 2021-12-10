using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BlogWebApi.Application.Mappers
{
    public class PostResponseMapper
    {
        public static PostResponseDto Map(Post post)
        {
            if (post == null)
            {
                return null;
            }

            return new PostResponseDto
            {
                BlogId = post.BlogId,
                PostId = post.PostId,
                PostName = post.PostName
            };
        }

        public static List<PostResponseDto> Map(IReadOnlyCollection<Post> posts)
        {
            return posts?.Select(post => new PostResponseDto
                {
                    BlogId = post.BlogId,
                    PostId = post.PostId,
                    PostName = post.PostName
                })
                .ToList();
        }
    }
}