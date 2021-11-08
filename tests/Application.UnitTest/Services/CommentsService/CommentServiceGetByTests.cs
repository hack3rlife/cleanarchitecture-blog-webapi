using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceGetByTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceGetByTests()
        {
            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "GetBy_CommentId_IsCalledOnce")]
        public async Task GetBy_CommentId_IsCalledOnce()
        {
            // Arrange
            var newComment = CommentBuilder.Default();
           await _mockCommentRepository.MockSetupGetByIdAsync(newComment);

            // Act
            var comment = await _commentService.GetBy(newComment.CommentId);

            // Assert
            Assert.NotNull(comment);
            _mockCommentRepository.MockVerifyGetByIdAsync(newComment, Times.Once());
        }

        [Fact(DisplayName = "GetBy_EmptyGuid_ThrowsArgumentNullException")]
        public async Task GetBy_EmptyGuid_ThrowsArgumentNullException()
        {
            // Arrange
            var commentId = Guid.Empty;

            // Act
           var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _commentService.GetBy(commentId));

            // Assert
           Assert.Equal("The commentId cannot be empty Guid. (Parameter 'commentId')", exception.Message);
        }
    }
}
