using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class BlogResponseDtoProfileMapperDto : ProfileMapperTestBase
    {
        [Fact]
        public void BlogMapper_ToBlogResponseDto_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var blog = new Blog
            {
                BlogId = guid,
                BlogName = "BlogName",
            };

            //Act
            var blogResponse = _mapper.Map<BlogResponseDto>(blog);

            //Assert
            Assert.Equal(blog.BlogId, blogResponse.BlogId);
            Assert.Equal(blog.BlogName, blogResponse.BlogName);
        }

        [Fact]
        public void BlogMapper_ToBlogResponseCollection_Success()
        {
            //Arrange
            var blogGuid1 = Guid.NewGuid();
            var blogGuid2 = Guid.NewGuid();

            var blogs = new List<Blog>()
            {
                new Blog
                {
                    BlogId = blogGuid1,
                    BlogName = "BlogName1",
                },
                new Blog
                {
                    BlogId = blogGuid2,
                    BlogName = "BlogName2",
                }
            };

            //Act
            var blogResponse = _mapper.Map<List<BlogResponseDto>>(blogs);

            //Assert
            Assert.Equal(2, blogResponse.Count);
            Assert.Collection(blogResponse, item =>
            {
                Assert.Equal(blogGuid1, item.BlogId);
                Assert.Equal("BlogName1", item.BlogName);
            },
                item =>
                {
                    Assert.Equal(blogGuid2, item.BlogId);
                    Assert.Equal("BlogName2", item.BlogName);
                });
        }
    }
}
