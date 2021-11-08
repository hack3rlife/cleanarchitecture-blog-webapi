using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
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

        [Fact(DisplayName = "Delete_CommentWithEmptyId_ThrowsArgumentNullException")]
        public async Task Delete_CommentWithEmptyId_ThrowsArgumentNullException()
        {
            // Arrange
            var commentId = Guid.Empty;
            
            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Delete(commentId));

            // Assert
            Assert.Equal("The commentId cannot be empty Guid. (Parameter 'commentId')", exception.Message);
        }

        [Fact(DisplayName = "Delete_CommentDoesNotExist_ThrowsArgumentNullException")]
        public async Task Delete_CommentDoesNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            await _mockCommentRepository.MockSetupGetByIdAsync(null);

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Delete(commentId));

            // Assert
            Assert.Equal($"The comment with {commentId} does not exist. (Parameter 'commentId')", exception.Message);
        }
    }
}