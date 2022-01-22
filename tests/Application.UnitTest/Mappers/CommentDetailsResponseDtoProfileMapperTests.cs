
using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class CommentDetailsResponseDtoProfileMapperTests : ProfileMapperTestBase
    {
        [Fact]
        public void CommentDetailsResponseMapper_MapToCommentDetailsResponseDto_Success()
        {
            // Arrange
            var comment = new Comment
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Sentence(10),
                Email = Lorem.Email(),
                CreatedBy = "hack3rlife",
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "hack3rlife",
                LastUpdate = DateTime.UtcNow
            };

            // Act
            var commentDetailsResponseDto = _mapper.Map<CommentDetailsResponseDto>(comment);

            // Assert
            Assert.Equal(comment.PostId, commentDetailsResponseDto.PostId);
            Assert.Equal(comment.CommentId, commentDetailsResponseDto.CommentId);
            Assert.Equal(comment.CommentName, commentDetailsResponseDto.CommentName);
            Assert.Equal(comment.Email, commentDetailsResponseDto.Email);
            Assert.Equal(comment.CreatedBy, commentDetailsResponseDto.CreatedBy);
            Assert.Equal(comment.UpdatedBy, commentDetailsResponseDto.UpdatedBy);
            Assert.Equal(comment.CreatedDate, commentDetailsResponseDto.CreatedDate);
            Assert.Equal(comment.LastUpdate, commentDetailsResponseDto.LastUpdate);
        }
    }
}
