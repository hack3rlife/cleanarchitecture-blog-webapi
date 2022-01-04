using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceDeleteTests : PostServiceBase
    {
        [Fact(DisplayName = "Delete_ExistingPost_IsCalledOnce")]
        public async Task Delete_ExistingPost_IsCalledOnce()
        {
            // Arrange
            var post = PostBuilder.Default();

            await MockPostRepository.MockSetupGetByIdAsync(post); 
            MockPostRepository.MockSetupDeleteAsync();

            //Act
            await PostService.Delete(post.PostId);

            //Assert
            MockPostRepository.MockVerifyDeleteAsync(post, Times.Once());
        }

        [Fact(DisplayName = "Delete_PostWithEmptyPostId_ThrowsBadRequestException")]
        public async Task Delete_PostWithEmptyPostId_ThrowsBadRequestException()
        {
            // Arrange
            var postId = Guid.Empty;

            //Act
            async Task Act() => await PostService.Delete(postId);

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);

            MockPostRepository.MockVerifyDeleteAsync(It.IsAny<Post>(), Times.Never());
        }

        [Fact(DisplayName = "Delete_NonExistingPost_NotFoundException")]
        public async Task Delete_NonExistingPost_NotFoundException()
        {
            // Arrange
            var postId = Guid.NewGuid();

            //Act
            async Task Act() => await PostService.Delete(postId);

            //Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(Act);
            Assert.Equal($"The postId: {postId} does not exist.", exception.Message);

            MockPostRepository.MockVerifyDeleteAsync(It.IsAny<Post>(), Times.Never());
        }
    }
}
