using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Mappers;
using BlogWebApi.Domain;
using LoremNET;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class BlogMapperTests
    {
        [Fact]
        public void BlogMapper_FromBlogRequestToBlog_Success()
        {
            //Arrange
            var blogAddRequestDto = new BlogAddRequestDto
            {
                BlogName = "BlogName",
                BlogId = Guid.NewGuid(),
                CreatedBy = "UnitTest"
            };

            //Act
            var blog = BlogMapper.FromBlogAddRequestDto(blogAddRequestDto);

            //Assert
            Assert.Equal(blogAddRequestDto.BlogName, blog.BlogName);
            Assert.Equal(blogAddRequestDto.BlogId, blog.BlogId);
            Assert.Equal(blogAddRequestDto.CreatedBy, blog.CreatedBy);
        }

        [Fact]
        public void BlogMapper_FromBlogRequestToBlog_ReturnsSNull()
        {
            //Arrange

            //Act
            var blog = BlogMapper.FromBlogAddRequestDto(null);

            //Assert
            Assert.Null(blog);
        }

        [Fact]
        public void BlogMapper_ToBlogByIdResponse_Success()
        {
            //Arrange
            var guid = Guid.NewGuid();

            var blog = new Blog
            {
                BlogId = guid,
                BlogName = "BlogName",
            };

            //Act
            var blogResponse = BlogMapper.ToBlogByIdResponseDto(blog);

            //Assert
            Assert.Equal(blog.BlogId, blogResponse.BlogId);
            Assert.Equal(blog.BlogName, blogResponse.BlogName);
        }

        [Fact]
        public void BlogMapper_ToBlogByIdResponse_ReturnsNull()
        {
            //Arrange

            //Act
            var blogResponse = BlogMapper.ToBlogByIdResponseDto(null);

            //Assert
            Assert.Null(blogResponse);
        }

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
            var blogResponse = BlogMapper.ToBlogResponse(blog);

            //Assert
            Assert.Equal(blog.BlogId, blogResponse.BlogId);
            Assert.Equal(blog.BlogName, blogResponse.BlogName);
            Assert.Equal(blog.CreatedBy, blogResponse.CreatedBy);
            Assert.Equal(blog.CreatedDate, blogResponse.CreatedDate);
            Assert.Equal(blog.UpdatedBy, blogResponse.UpdatedBy);
            Assert.Equal(blog.LastUpdate, blogResponse.LastUpdate);
            Assert.Equal(blog.Post.Count, blogResponse.Post.Count);
        }

        [Fact]
        public void BlogMapper_ToBlogResponse_ReturnsNull()
        {
            //Arrange
           
            //Act
            var blogResponse = BlogMapper.ToBlogResponse(blog: null);

            //Assert
            Assert.Null(blogResponse);
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
                    CreatedBy = "UnitTester",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = "UnitTester",
                    LastUpdate = DateTime.UtcNow,

                },
                new Blog
                {
                    BlogId = blogGuid2,
                    BlogName = "BlogName2",
                    CreatedBy = "UnitTester2",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = "UnitTester2",
                    LastUpdate = DateTime.UtcNow,
                }
            };

            //Act
            var blogResponse = BlogMapper.ToBlogCollectionResponse(blogs);

            //Assert
            Assert.Equal(2, blogResponse.Count);
            Assert.Collection(blogResponse, item =>
                {
                    Assert.Equal(blogGuid1, item.BlogId);
                    Assert.Equal("BlogName1", item.BlogName);
                    Assert.Equal("UnitTester", item.CreatedBy);
                    Assert.Equal("UnitTester", item.UpdatedBy);
                },
                item =>
                {
                    Assert.Equal(blogGuid2, item.BlogId);
                    Assert.Equal("BlogName2", item.BlogName);
                    Assert.Equal("UnitTester2", item.UpdatedBy);
                    Assert.Equal("UnitTester2", item.CreatedBy);
                });
        }
    }
}
