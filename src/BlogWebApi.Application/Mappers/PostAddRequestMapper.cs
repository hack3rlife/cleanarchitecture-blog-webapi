using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Mappers
{
    public static class PostAddRequestMapper
    {
        public static Post Map(PostAddRequestDto postAddRequestDto)
        {
            if (postAddRequestDto == null)
            {
                return null;
            }

            return new Post
            {
                BlogId = postAddRequestDto.BlogId,
                PostName = postAddRequestDto.PostName,
                PostId = postAddRequestDto.PostId,
                CreatedBy = postAddRequestDto.CreatedBy,
                Text = postAddRequestDto.Text
            };
        }
    }
}
