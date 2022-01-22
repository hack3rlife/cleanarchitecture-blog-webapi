using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostAddRequestDtoProfileMapperTests : ProfileMapperTestBase
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
            var post = _mapper.Map<Post>(postAddRequestDto);

            Assert.Equal(postAddRequestDto.BlogId, post.BlogId);
            Assert.Equal(postAddRequestDto.CreatedBy, post.CreatedBy);
            Assert.Equal(postAddRequestDto.PostId, post.PostId);
            Assert.Equal(postAddRequestDto.PostName, post.PostName);
            Assert.Equal(postAddRequestDto.Text, post.Text);
        }
    }
}