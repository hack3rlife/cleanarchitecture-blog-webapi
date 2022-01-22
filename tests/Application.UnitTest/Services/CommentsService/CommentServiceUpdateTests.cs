using Application.UnitTest.Builders;
using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.CommentsService
{
    public class CommentServiceUpdateTests : CommentServiceBase
    {
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

        [Fact(DisplayName = "Update_CommentNotExistent_NotFoundException")]
        public async Task Update_CommentNotExistent_NotFoundException()
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
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal($"The comment: {newCommentUpdateRequestDto.CommentId} does not exist.", exception.Message);

            _mockCommentRepository.MockVerifyUpdateAsync(Times.Never());
        }

        [Fact(DisplayName = "Update_NullComment_ThrowsBadRequestException")]
        public async Task Update_NullComment_ThrowsBadRequestException()
        {
            //Arrange

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Update(null));

            // Assert
            Assert.Equal("The comment cannot be null.", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyCommentId_ThrowsBadRequestException")]
        public async Task Update_EmptyCommentId_ThrowsBadRequestException()
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
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The commentId cannot be empty Guid.", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsBadRequestException(string name)
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentName = name,
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                Email = Lorem.Email()
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The comment name cannot be null or empty.", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullEmail_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullEmail_ThrowsBadRequestException(string email)
        {
            //Arrange
            var newCommentUpdateRequestDto = new CommentUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                CommentId = Guid.NewGuid(),
                UpdatedBy = "hack3rlife",
                CommentName = Lorem.Sentence(5),
                Email = email
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The email cannot be null or empty.", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyPostId_ThrowsBadRequestException")]
        public async Task Update_EmptyPostId_ThrowsBadRequestException()
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
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await _commentService.Update(newCommentUpdateRequestDto));

            // Assert
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);
        }
    }
}