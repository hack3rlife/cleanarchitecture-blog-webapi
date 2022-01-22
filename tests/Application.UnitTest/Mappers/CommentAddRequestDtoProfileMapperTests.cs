using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class CommentAddRequestDtoProfileMapperTests : ProfileMapperTestBase
    {
        [Fact]
        public void CommentMapper_MapFromCommentAddRequestDto_Success()
        {
            // Arrange
            var commentAddRequestDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(10),
                Email = Lorem.Email(),
                CreatedBy = "hack3rlife",
                CreatedDate = DateTime.UtcNow
            };

            // Act
            var comment = _mapper.Map<Comment>(commentAddRequestDto);

            // Assert
            Assert.Equal(commentAddRequestDto.PostId, comment.PostId);
            Assert.Equal(commentAddRequestDto.CommentId, comment.CommentId);
            Assert.Equal(commentAddRequestDto.CommentName, comment.CommentName);
            Assert.Equal(commentAddRequestDto.Email, comment.Email);
            Assert.Equal(commentAddRequestDto.CreatedBy, comment.CreatedBy);
            Assert.Equal(commentAddRequestDto.CreatedDate, comment.CreatedDate);
        }

       
    }
}
