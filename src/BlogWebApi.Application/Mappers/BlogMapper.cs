using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace BlogWebApi.Application.Mappers
{
    public static class BlogMapper
    {

        public static Blog FromBlogAddRequestDto(BlogAddRequestDto blogAddRequestDto)
        {
            if (blogAddRequestDto == null)
            {
                return null;
            }

            return new Blog
            {
                BlogId = blogAddRequestDto.BlogId,
                BlogName = blogAddRequestDto.BlogName,
                CreatedBy = blogAddRequestDto.CreatedBy
            };
        }

        public static BlogByIdResponseDto ToBlogByIdResponseDto(Blog blog)
        {
            if (blog == null)
            {
                return null;
            }

            return new BlogByIdResponseDto
            {
                BlogId = blog.BlogId,
                BlogName = blog.BlogName,
            };
        }

        public static BlogDetailsResponseDto ToBlogResponse(Blog blog)
        {
            if (blog == null)
            {
                return null;
            }

            return new BlogDetailsResponseDto
            {
                BlogId = blog.BlogId,
                BlogName = blog.BlogName,
                CreatedDate = blog.CreatedDate,
                CreatedBy = blog.CreatedBy,
                LastUpdate = blog.LastUpdate,
                UpdatedBy = blog.UpdatedBy,
                Post = blog.Post
            };
        }

        public static List<BlogDetailsResponseDto> ToBlogCollectionResponse(IReadOnlyCollection<Blog> blogs)
        {
            return blogs?.Select(blog => new BlogDetailsResponseDto
                {
                    BlogId = blog.BlogId,
                    BlogName = blog.BlogName,
                    CreatedDate = blog.CreatedDate,
                    CreatedBy = blog.CreatedBy,
                    LastUpdate = blog.LastUpdate,
                    UpdatedBy = blog.UpdatedBy,
                    Post = blog.Post
                })
                .ToList();
        }
    }
}