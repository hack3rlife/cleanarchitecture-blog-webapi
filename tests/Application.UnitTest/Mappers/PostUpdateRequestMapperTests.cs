using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Mappers;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostUpdateRequestMapperTests
    {
        [Fact]
        public void PostUpdateRequestMapper_MapToPost_Success()
        {
            // Arrange
            var postUpdateRequestDto = new PostUpdateRequestDto
            {
                BlogId = Guid.NewGuid(),
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(5),
                Text = Lorem.Sentence(10),
                UpdatedBy = "hack3rlife"
            };

            // Act
            var post = PostUpdateRequestMapper.Map(postUpdateRequestDto);

            // Assert
            Assert.Equal(postUpdateRequestDto.BlogId, post.BlogId);
            Assert.Equal(postUpdateRequestDto.PostId, post.PostId);
            Assert.Equal(postUpdateRequestDto.PostName, post.PostName);
            Assert.Equal(postUpdateRequestDto.Text, post.Text);
            Assert.Equal(postUpdateRequestDto.UpdatedBy, post.UpdatedBy);
        }

        [Fact]
        public void PostUpdateRequestMapper_WhenPostUpdateRequestDto_ReturnsNullPost()
        {
            // Arrange
            PostUpdateRequestDto postUpdateRequestDto = null;

            // Act
            var post = PostUpdateRequestMapper.Map(postUpdateRequestDto);

            // Assert
            Assert.Null(post);
        }
    }
}
