using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceGetByTests : CommentServiceBase
    {
        [Fact(DisplayName = "GetBy_CommentId_IsCalledOnce")]
        public async Task GetBy_CommentId_IsCalledOnce()
        {
            // Arrange
            var newComment = new Comment
            {
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(10),
                Email = Lorem.Email(),
                PostId = Guid.NewGuid()
            };

            await _mockCommentRepository.MockSetupGetByIdAsync(newComment);

            // Act
            var comment = await _commentService.GetBy(newComment.CommentId);

            // Assert
            Assert.NotNull(comment);
            _mockCommentRepository.MockVerifyGetByIdAsync(newComment, Times.Once());
        }

        [Fact(DisplayName = "GetBy_EmptyGuid_ThrowsBadRequestException")]
        public async Task GetBy_EmptyGuid_ThrowsBadRequestException()
        {
            // Arrange
            var commentId = Guid.Empty;

            // Act
           var exception = await Assert.ThrowsAsync<BadRequestException>(() => _commentService.GetBy(commentId));

            // Assert
           Assert.Equal("The commentId cannot be empty Guid.", exception.Message);
        }
    }
}
