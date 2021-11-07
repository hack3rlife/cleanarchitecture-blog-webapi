using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceUpdateTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceUpdateTests()
        {
            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "Update_Comment_IsCalledOnce")]
        public async Task Update_Comment_IsCalledOnce()
        {
            // Arrange
            var newComment = CommentBuilder.Default();
            await _mockCommentRepository.MockSetupGetByIdAsync(newComment);
            _mockCommentRepository.MockSetupUpdateAsync();

            // Act
            await _commentService.Update(newComment);

            // Assert
            _mockCommentRepository.MockVerifyUpdateAsync(Times.Once());
        }
    }
}
