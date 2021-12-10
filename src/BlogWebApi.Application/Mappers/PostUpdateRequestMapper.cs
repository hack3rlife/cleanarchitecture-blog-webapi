using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Mappers
{
    public class PostUpdateRequestMapper
    {
        public static Post Map(PostUpdateRequestDto postUpdateRequestDto)
        {
            if (postUpdateRequestDto == null)
            {
                return null;
            }

            return new Post
            {
                BlogId = postUpdateRequestDto.BlogId,
                PostName = postUpdateRequestDto.PostName,
                Text = postUpdateRequestDto.Text,
                PostId = postUpdateRequestDto.PostId,
                UpdatedBy = postUpdateRequestDto.UpdatedBy
            };
        }
    }
}
