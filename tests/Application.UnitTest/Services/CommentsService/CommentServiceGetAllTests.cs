using Application.UnitTest.Mocks;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceGetAllTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly CommentService _commentService;

        private const int skip = 0; //default pagination value
        private const int take = 10; //default pagination value

        public CommentServiceGetAllTests()
        {
            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "GetAll_WithoutPagingValues_UsesDefaultPagingValues")]
        public async Task GetAll_WithoutPagingValues_UsesDefaultPagingValues()
        {
            // Arrange
            await _mockCommentRepository.MockSetupListAllAsync(new List<Comment>());

            // Act
            var blogs = await _commentService.GetAll();

            // Assert
            Assert.NotNull(blogs);
            _mockCommentRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_CommentsUsingInvalidSkipValue_UsesDefaultValueForSkip")]
        public async Task GetAll_CommentsUsingInvalidSkipValue_UsesDefaultValueForSkip()
        {
            // Arrange
            await _mockCommentRepository.MockSetupListAllAsync(new List<Comment>());

            // Act
            var blogs = await _commentService.GetAll(-10);

            // Assert
            Assert.NotNull(blogs);
            _mockCommentRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_CommentsUsingInvalidTakeValue_UsesDefaultValueForTake")]
        public async Task GetAll_CommentsUsingInvalidTakeValue_UsesDefaultValueForTake()
        {
            // Arrange
            await _mockCommentRepository.MockSetupListAllAsync(new List<Comment>());

            // Act
            var blogs = await _commentService.GetAll(take: -10);

            // Assert
            Assert.NotNull(blogs);
            _mockCommentRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }

        [Theory(DisplayName = "GetAll_CommentUsingPagination_IsCalledOnce")]
        [InlineData(1, 1)]
        [InlineData(10, 100)]
        [InlineData(100, 1000)]
        public async Task GetAll_CommentUsingPagination_IsCalledOnce(int skip, int take)
        {
            // Arrange
            await _mockCommentRepository.MockSetupListAllAsync(new List<Comment>());

            // Act
            var blogs = await _commentService.GetAll(skip,take);

            // Assert
            Assert.NotNull(blogs);
            _mockCommentRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }
    }
}
