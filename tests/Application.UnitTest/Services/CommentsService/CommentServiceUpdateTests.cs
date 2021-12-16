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
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email()
            };

            var newComment = CommentBuilder.Default();
            await _mockCommentRepository.MockSetupGetByIdAsync(newComment);
            _mockCommentRepository.MockSetupUpdateAsync();

            // Act
            await _commentService.Update(newCommentUpdateRequestDto);

            // Assert
            _mockCommentRepository.MockVerifyUpdateAsync(Times.Once());
        }

        [Fact(DisplayName = "Update_CommentNotExistent_ThrowsArgumentException")]
        public async Task Update_CommentNotExistent_ThrowsArgumentException()
        {
            // Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email()
            };

            await _mockCommentRepository.MockSetupGetByIdAsync(null);
            _mockCommentRepository.MockSetupUpdateAsync();

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal($"The comment with {newCommentUpdateRequestDto.CommentId} does not exist. (Parameter 'CommentId')", exception.Message);

            _mockCommentRepository.MockVerifyUpdateAsync(Times.Never());
        }

        [Fact(DisplayName = "Update_NullComment_ThrowsArgumentNullException")]
        public async Task Update_NullComment_ThrowsArgumentNullException()
        {
            //Arrange
           
            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Update(null));

            // Assert
            Assert.Equal("The comment cannot be null. (Parameter 'commentUpdateRequestDto')", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyCommentId_ThrowsArgumentNullException")]
        public async Task Update_EmptyCommentId_ThrowsArgumentNullException()
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email()
            };

            var comment = CommentBuilder.Default();
            comment.CommentId = Guid.Empty;

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The commentId cannot be empty Guid. (Parameter 'commentUpdateRequestDto')", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsArgumentNullException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsArgumentNullException(string name)
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                Email = Lorem.Email()
            };

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The comment name cannot be null or empty. (Parameter 'commentUpdateRequestDto')", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullEmail_ThrowsArgumentNullException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullEmail_ThrowsArgumentNullException(string email)
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
            };

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The email cannot be null or empty. (Parameter 'commentUpdateRequestDto')", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyPostId_ThrowsArgumentNullException")]
        public async Task Update_EmptyPostId_ThrowsArgumentNullException()
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email()
            };

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'commentUpdateRequestDto')", exception.Message);
        }
    }
}
