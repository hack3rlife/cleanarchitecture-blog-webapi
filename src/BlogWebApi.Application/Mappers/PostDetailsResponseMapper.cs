using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Mappers
{
    public static class PostDetailsResponseMapper
    {
        public static PostDetailsResponseDto Map(Post post)
        {
            if (post == null)
            {
                return null;
            }

            return new PostDetailsResponseDto
            {
                BlogId = post.BlogId,
                PostName = post.PostName,
                Text = post.Text,
                Comment = post.Comment,
                PostId = post.PostId,
                CreatedBy = post.CreatedBy,
                CreatedDate = post.CreatedDate,
                UpdatedBy = post.UpdatedBy,
                LastUpdate = post.LastUpdate
            };
        }
    }
}
