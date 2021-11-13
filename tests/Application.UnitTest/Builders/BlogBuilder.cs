using System;
using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;

namespace Application.UnitTest.Builders
{
    public class BlogBuilder
    {
        private readonly BlogAddRequestDto _blog;

        private BlogBuilder()
        {
            _blog = new BlogAddRequestDto();
        }

        public static BlogBuilder Create()
        {
            return new BlogBuilder();
        }

        public static BlogUpdateRequestDto DefaultForBlogUpdateRequestDto()
        {
            return new BlogUpdateRequestDto
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };
        }

        public static Blog Default()
        {
            return new Blog
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };
        }
    }
}