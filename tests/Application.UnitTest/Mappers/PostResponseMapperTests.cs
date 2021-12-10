using BlogWebApi.Application.Mappers;
using BlogWebApi.Domain;
using LoremNET;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostResponseMapperTests
    {
        [Fact]
        public void PostResponseMapper_MapToPostResponseDto_Success()
        {
            // Act
            var post = new Post
            {
                BlogId = Guid.NewGuid(),
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
            };

            // Arrange
            var postResponseDto = PostResponseMapper.Map(post);

            // Assert 
            Assert.Equal(post.BlogId, postResponseDto.BlogId);
            Assert.Equal(post.PostId, postResponseDto.PostId);
            Assert.Equal(post.PostName, postResponseDto.PostName);
        }

        [Fact]
        public void PostResponseMapper_WhenPostIsNull_ReturnsNullPostResponseDto()
        {
            // Act
            Post post = null;

            // Arrange
            var postResponseDto = PostResponseMapper.Map(post);

            // Assert 
            Assert.Null(postResponseDto);
        }

        [Fact]
        public void PostResponseMapper_MapToPostResponseDtoCollection_Success()
        {
            // Act
            var blogId = Guid.NewGuid();

            var posts = new List<Post>
            {
                new Post
                {
                    BlogId = blogId,
                    PostId = Guid.NewGuid(),
                    PostName = Lorem.Words(10),
                },
                new Post
                {
                    BlogId = blogId,
                    PostId = Guid.NewGuid(),
                    PostName = Lorem.Words(10),
                }
            };

            // Arrange
            var postResponseDto = PostResponseMapper.Map(posts);

            // Assert 
            Assert.Equal(posts.Count, postResponseDto.Count);
            Assert.Collection(postResponseDto, item =>
                {
                    Assert.Equal(blogId, item.BlogId);
                    Assert.Equal(posts[0].PostId, item.PostId);
                    Assert.Equal(posts[0].PostName, item.PostName);
                },
                item =>
                {
                    Assert.Equal(blogId, item.BlogId);
                    Assert.Equal(posts[1].PostId, item.PostId);
                    Assert.Equal(posts[1].PostName, item.PostName);
                });
        }

        [Fact]
        public void PostResponseMapper_WhenPostCollectionIsNull_ReturnsNullostResponseDtoCollection()
        {
            // Act

            List<Post> posts = null;

            // Arrange
            var postResponseDto = PostResponseMapper.Map(posts);

            // Assert 
            Assert.Null(postResponseDto);
        }
    }
}