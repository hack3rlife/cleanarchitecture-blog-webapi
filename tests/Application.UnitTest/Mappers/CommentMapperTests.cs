using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Mappers;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class CommentMapperTests
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
            var comment = CommentMapper.Map(commentAddRequestDto);

            // Assert
            Assert.Equal(commentAddRequestDto.PostId, comment.PostId);
            Assert.Equal(commentAddRequestDto.CommentId, comment.CommentId);
            Assert.Equal(commentAddRequestDto.CommentName, comment.CommentName);
            Assert.Equal(commentAddRequestDto.Email, comment.Email);
            Assert.Equal(commentAddRequestDto.CreatedBy, comment.CreatedBy);
            Assert.Equal(commentAddRequestDto.CreatedDate, comment.CreatedDate);
        }

        [Fact]
        public void CommentMapper_MapFromCommentAddRequestDto_ReturnsNull()
        {
            // Arrange
            CommentAddRequestDto commentAddRequestDto = null;

            // Act
            var comment = CommentMapper.Map(commentAddRequestDto);

            // Assert
            Assert.Null(comment);
        }

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
            var comment = CommentMapper.Map(commentUpdateRequestDto);

            // Assert
            Assert.Equal(commentUpdateRequestDto.PostId, comment.PostId);
            Assert.Equal(commentUpdateRequestDto.CommentId, comment.CommentId);
            Assert.Equal(commentUpdateRequestDto.CommentName, comment.CommentName);
            Assert.Equal(commentUpdateRequestDto.Email, comment.Email);
            Assert.Equal(commentUpdateRequestDto.UpdatedBy, comment.UpdatedBy);
            Assert.Equal(commentUpdateRequestDto.LastUpdate, comment.LastUpdate);
        }

        [Fact]
        public void CommentMapper_MapFromCommentUpdateRequestDto_ReturnsNull()
        {
            // Arrange
            CommentUpdateRequestDto commentUpdateRequestDto = null;

            // Act
            var comment = CommentMapper.Map(commentUpdateRequestDto);

            // Assert
            Assert.Null(comment);
        }
    }
}
