using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceDeleteTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceDeleteTests()
        {
            _mockCommentRepository = new  MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "Delete_Comment_IsCalledOnce")]
        public async Task Delete_Comment_IsCalledOnce()
        {
            // Arrange
            var newComment = CommentBuilder.Default();
            await _mockCommentRepository.MockSetupGetByIdAsync(newComment);
            _mockCommentRepository.MockSetupDeleteAsync();

            // Act
            await _commentService.Delete(newComment.CommentId);

            // Assert
            _mockCommentRepository.MockVerifyDeleteAsync(Times.Once());
        }

        [Fact(DisplayName = "Delete_CommentWithEmptyId_ThrowsBadRequestException")]
        public async Task Delete_CommentWithEmptyId_ThrowsBadRequestException()
        {
            // Arrange
            var commentId = Guid.Empty;
            
            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Delete(commentId));

            // Assert
            Assert.Equal("The commentId cannot be empty Guid.", exception.Message);
        }

        [Fact(DisplayName = "Delete_CommentDoesNotExist_ThrowsBadRequestException")]
        public async Task Delete_CommentDoesNotExist_ThrowsBadRequestException()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            await _mockCommentRepository.MockSetupGetByIdAsync(null);

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _commentService.Delete(commentId));

            // Assert
            Assert.Equal($"The comment: {commentId} does not exist.", exception.Message);
        }
    }
}