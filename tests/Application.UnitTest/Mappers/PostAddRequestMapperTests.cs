using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Mappers;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostAddRequestMapperTests
    {
        [Fact]
        public void PostAddRequestMapper_MapToPost_Success()
        {
            // Arrange
            var postAddRequestDto = new PostAddRequestDto
            {
                BlogId = Guid.NewGuid(),
                CreatedBy = "hack3rlife",
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(5)
            };

            // Act
            var post = PostAddRequestMapper.Map(postAddRequestDto);

            Assert.Equal(post.BlogId, postAddRequestDto.BlogId);
            Assert.Equal(post.CreatedBy, postAddRequestDto.CreatedBy);
            Assert.Equal(post.PostId, postAddRequestDto.PostId);
            Assert.Equal(post.PostName, postAddRequestDto.PostName);
            Assert.Equal(post.Text, postAddRequestDto.Text);
        }

        [Fact]
        public void PostAddRequestMapper_WhenPostAddRequestIsNull_ReturnsNullPost()
        {
            // Arrange
            PostAddRequestDto postAddRequestDto = null;

            // Act
            var post = PostAddRequestMapper.Map(postAddRequestDto);

            Assert.Null(post);
        }
    }
}