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
    }
}