using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using BlogWebApi.Domain;
using LoremNET;
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
            var newCommentDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                Email = Lorem.Email(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(50)
            };

            await _mockCommentRepository.MockSetupAddAsync(new Comment());

            // Act
            var comment = await _commentService.Add(newCommentDto);

            // Assert
            Assert.NotNull(comment);
            _mockCommentRepository.MockVerifyAddAsync(Times.Once());
        }

        [Fact(DisplayName = "Add_CommentIsNull_ThrowsArgumentNullException")]
        public async Task Add_CommentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            CommentAddRequestDto newComment = null;

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Add(newComment));
            
            // Assert
            Assert.Equal("The comment cannot be null. (Parameter 'commentAddRequestDto')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyName_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyName_ThrowsArgumentNullException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                Email = Lorem.Email(),
                CommentId = Guid.NewGuid(),
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The comment name cannot be null or empty. (Parameter 'commentAddRequestDto')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyEmail_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyEmail_ThrowsArgumentNullException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(50)
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The email cannot be null or empty. (Parameter 'commentAddRequestDto')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyPostId_ThrowsArgumentNullException")]
        public async Task Add_CommentWithEmptyPostId_ThrowsArgumentNullException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                Email = Lorem.Email(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(50)
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'commentAddRequestDto')", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }
    }
}