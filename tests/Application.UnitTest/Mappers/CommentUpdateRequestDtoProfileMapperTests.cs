using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class CommentUpdateRequestDtoProfileMapperTests : ProfileMapperTestBase 
    {
        [Fact]
        public void CommentMapper_MapFromCommentUpdateRequestDto_Success()
        {
            // Arrange
            var commentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(10),
                Email = Lorem.Email(),
                UpdatedBy = "hack3rlife",
                LastUpdate = DateTime.UtcNow
            };

            // Act
            var comment = _mapper.Map<Comment>(commentUpdateRequestDto);

            // Assert
            Assert.Equal(commentUpdateRequestDto.PostId, comment.PostId);
            Assert.Equal(commentUpdateRequestDto.CommentId, comment.CommentId);
            Assert.Equal(commentUpdateRequestDto.CommentName, comment.CommentName);
            Assert.Equal(commentUpdateRequestDto.Email, comment.Email);
            Assert.Equal(commentUpdateRequestDto.UpdatedBy, comment.UpdatedBy);
            Assert.Equal(commentUpdateRequestDto.LastUpdate, comment.LastUpdate);
        }
    }
}
