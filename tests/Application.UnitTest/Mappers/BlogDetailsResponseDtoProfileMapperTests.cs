using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using System.Collections.Generic;
using Xunit;
namespace Application.UnitTest.Mappers
{
    public class BlogDetailsResponseDtoProfileMapperTests : ProfileMapperTestBase
    {
        [Fact]
        public void BlogMapper_ToBlogResponse_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var blog = new Blog
            {
                BlogId = guid,
                BlogName = "BlogName",
                CreatedBy = "UnitTester",
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "UnitTester",
                LastUpdate = DateTime.UtcNow,
                Post = new List<Post>
                {
                    new Post {BlogId = guid, PostId = Guid.NewGuid(), PostName = Lorem.Words(10)},
                    new Post {BlogId = guid, PostId = Guid.NewGuid(), PostName = Lorem.Words(10)},
                    new Post {BlogId = guid, PostId = Guid.NewGuid(), PostName = Lorem.Words(10)},
                    new Post {BlogId = guid, PostId = Guid.NewGuid(), PostName = Lorem.Words(10)},
                    new Post {BlogId = guid, PostId = Guid.NewGuid(), PostName = Lorem.Words(10)},
                }
            };

            //Act
            var blogResponse = _mapper.Map<BlogDetailsResponseDto>(blog);

            //Assert
            Assert.Equal(blog.BlogId, blogResponse.BlogId);
            Assert.Equal(blog.BlogName, blogResponse.BlogName);
            Assert.Equal(blog.CreatedBy, blogResponse.CreatedBy);
            Assert.Equal(blog.CreatedDate, blogResponse.CreatedDate);
            Assert.Equal(blog.UpdatedBy, blogResponse.UpdatedBy);
            Assert.Equal(blog.LastUpdate, blogResponse.LastUpdate);
            Assert.Equal(blog.Post.Count, blogResponse.Post.Count);
        }
    }
}
