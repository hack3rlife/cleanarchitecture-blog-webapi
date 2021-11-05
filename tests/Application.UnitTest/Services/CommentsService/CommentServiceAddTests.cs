using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceAddTests
    {
        private readonly MockCommentsRepository _mockCommentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceAddTests()
        {
            _mockCommentRepository = new MockCommentsRepository();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact(DisplayName = "Add_Comment_IsCalledOnce")]
        public async Task Add_Comment_IsCalledOnce()
        {
            // Arrange
            var newComment = CommentBuilder.Default();
            await _mockCommentRepository.MockSetupAddAsync(newComment);

            // Act
            var comment = await _commentService.Add(newComment);

            // Assert
            Assert.NotNull(comment);
            _mockCommentRepository.MockVerifyAddAsync(newComment, Times.Once());
        }

        [Fact(DisplayName = "Add_CommentIsNull_ThrowsArgumentNullException")]
        public async Task Add_CommentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Comment newComment = null;

            // Act
            async Task Comment() => await _commentService.Add(newComment);

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Comment);
            Assert.Equal("The comment cannot be null. (Parameter 'comment')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(newComment, Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyName_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyName_ThrowsArgumentNullException()
        {
            // Arrange
            var newComment = CommentBuilder.Create()
                .WithRandomGuid()
                .WithRandomEmail()
                .WithRandomPostId()
                .Build();

            // Act
            async Task Comment() => await _commentService.Add(newComment);

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Comment);
            Assert.Equal("The comment name cannot be null or empty. (Parameter 'comment')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(newComment, Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyEmail_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyEmail_ThrowsArgumentNullException()
        {
            // Arrange
            var newComment = CommentBuilder.Create()
                .WithRandomGuid()
                .WithRandomCommandName()
                .WithRandomPostId()
                .Build();

            // Act
            async Task Comment() => await _commentService.Add(newComment);

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Comment);
            Assert.Equal("The email cannot be null or empty. (Parameter 'comment')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(newComment, Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyPostId_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyPostId_ThrowsArgumentNullException()
        {
            // Arrange
            var newComment = CommentBuilder.Create()
                .WithRandomGuid()
                .WithRandomCommandName()
                .WithRandomEmail()
                .Build();

            // Act
            async Task Comment() => await _commentService.Add(newComment);

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Comment);
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'comment')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(newComment, Times.Never());
        }
    }
}