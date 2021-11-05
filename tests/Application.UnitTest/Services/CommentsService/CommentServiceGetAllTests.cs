using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceGetAllTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceGetAllTests()
        {
            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "GetAll_CommentUsingPagination_IsCalledOnce")]
        public async Task GetAll_CommentUsingPagination_IsCalledOnce()
        {
            // Arrange
            var skip = 0;
            var take = 10;

            await _mockCommentRepository.MockSetupListAllAsync(new List<Comment>());

            // Act
            var blogs = await _commentService.GetAll(skip,take);

            // Assert
            Assert.NotNull(blogs);
            _mockCommentRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }
    }
}
