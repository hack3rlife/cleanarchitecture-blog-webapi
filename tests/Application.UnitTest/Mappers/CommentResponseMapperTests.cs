using BlogWebApi.Application.Mappers;
using BlogWebApi.Domain;
using LoremNET;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class CommentResponseMapperTests
    {
        [Fact]
        public void CommentResponseMapper_MapFromComment_Success()
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
            var commentResponseDto = CommentResponseMapper.Map(comment);

            // Assert
            Assert.Equal(comment.CommentId, commentResponseDto.CommentId);
            Assert.Equal(comment.CommentName, commentResponseDto.CommentName);
            Assert.Equal(comment.PostId, commentResponseDto.PostId);
        }

        [Fact]
        public void CommentResponseMapper_MapFromNullComment_ReturnsNull()
        {
            // Arrange
            Comment comment = null;

            // Act
            var commentResponseDto = CommentResponseMapper.Map(comment);

            // Assert
            Assert.Null(commentResponseDto);
        }

        [Fact]
        public void CommentResponseMapper_MapFromComments_Success()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment
                {
                    PostId = Guid.NewGuid(),
                    CommentId = Guid.NewGuid(),
                    CommentName = Lorem.Sentence(10),
                    Email = Lorem.Email(),
                    CreatedBy = "hack3rlife",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = "hack3rlife",
                    LastUpdate = DateTime.UtcNow
                },
                new Comment
                {
                    PostId = Guid.NewGuid(),
                    CommentId = Guid.NewGuid(),
                    CommentName = Lorem.Sentence(10),
                    Email = Lorem.Email(),
                    CreatedBy = "hack3rlife",
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = "hack3rlife",
                    LastUpdate = DateTime.UtcNow
                }
            };

            // Act
            var commentResponseDto = CommentResponseMapper.Map(comments).ToList();

            // Assert
            Assert.Collection(commentResponseDto,
                item =>
                {
                    Assert.Equal(item.PostId, comments[0].PostId);
                    Assert.Equal(item.CommentName, comments[0].CommentName);
                    Assert.Equal(item.CommentId, comments[0].CommentId);
                }, item =>
                {
                    Assert.Equal(item.PostId, comments[1].PostId);
                    Assert.Equal(item.CommentName, comments[1].CommentName);
                    Assert.Equal(item.CommentId, comments[1].CommentId);
                });
        }

        [Fact]
        public void CommentResponseMapper_MapFromNullComments_Success()
        {
            // Arrange
            List<Comment> comments = null;

            // Act
            var commentResponseDto = CommentResponseMapper.Map(comments)?.ToList();

            // Assert
            Assert.Null(comments);
        }
    }
}