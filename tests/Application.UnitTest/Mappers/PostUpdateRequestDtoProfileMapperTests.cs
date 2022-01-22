using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostUpdateRequestDtoProfileMapperTests : ProfileMapperTestBase
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
            var post = _mapper.Map<Post>(postUpdateRequestDto);

            // Assert
            Assert.Equal(postUpdateRequestDto.BlogId, post.BlogId);
            Assert.Equal(postUpdateRequestDto.PostId, post.PostId);
            Assert.Equal(postUpdateRequestDto.PostName, post.PostName);
            Assert.Equal(postUpdateRequestDto.Text, post.Text);
            Assert.Equal(postUpdateRequestDto.UpdatedBy, post.UpdatedBy);
        }
    }
}
