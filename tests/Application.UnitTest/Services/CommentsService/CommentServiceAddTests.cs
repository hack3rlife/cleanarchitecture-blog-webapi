using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceAddTests : CommentServiceBase
    {
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

        [Fact(DisplayName = "Add_CommentIsNull_ThrowsBadRequestException")]
        public async Task Add_CommentIsNull_ThrowsBadRequestException()
        {
            // Arrange
            CommentAddRequestDto newComment = null;

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Add(newComment));
            
            // Assert
            Assert.Equal("The comment cannot be null.", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyName_ThrowsBadRequestException")]
        public async Task Add_CommentWithEmptyName_ThrowsBadRequestException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                Email = Lorem.Email(),
                CommentId = Guid.NewGuid(),
            };

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The comment name cannot be null or empty.", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyEmail_ThrowsBadRequestException")]
        public async Task Add_CommentWithEmptyEmail_ThrowsBadRequestException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(50)
            };

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The email cannot be null or empty.", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }

        [Fact(DisplayName = "Add_CommentWithEmptyPostId_ThrowsBadRequestException")]
        public async Task Add_CommentWithEmptyPostId_ThrowsBadRequestException()
        {
            // Arrange
            var newCommentDto = new CommentAddRequestDto
            {
                Email = Lorem.Email(),
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Words(50)
            };

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Add(newCommentDto));

            // Assert
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);

            _mockCommentRepository.MockVerifyAddAsync(Times.Never());
        }
    }
}