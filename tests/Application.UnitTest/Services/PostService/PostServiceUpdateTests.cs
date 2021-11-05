using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceUpdateTests : PostServiceBase
    {
        [Fact(DisplayName = "Update_Post_IsCalledOnce")]
        public async Task Update_Post_IsCalledOnce()
        {
            //Arrange
            var updatePost = PostBuilder.Default();

            await MockPostRepository.MockSetupGetByIdAsync(updatePost);

            MockPostRepository.MockSetupUpdateAsync();

            //Act
            await PostService.Update(updatePost);

            //Assert
            MockPostRepository.MockVerifyUpdateAsync(updatePost, Times.Once());
        }

        [Fact(DisplayName = "Update_NonExistentPost_ThrowsArgumentException")]
        public async Task Update_NonExistentPost_IsCalledOnce()
        {
            //Arrange
            var updatePost = PostBuilder.Default();

            await MockPostRepository.MockSetupGetByIdAsync(null);
            MockPostRepository.MockSetupUpdateAsync();

            //Act
            async Task Act() => await PostService.Update(updatePost);
            var exception = await Assert.ThrowsAsync<ArgumentException>(Act);

            //Assert
            Assert.Equal($"The post with {updatePost.PostId} does not exist. (Parameter 'PostId')", exception.Message);

            MockPostRepository.MockVerifyUpdateAsync(updatePost, Times.Never());
        }


        [Fact(DisplayName = "Update_PostUsingLongName_ThrowsArgumentOutOfRangeException")]
        public async Task Update_PostUsingLongName_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var updatePost = PostBuilder.WithLongName();

            //Act
            async Task Act() => await PostService.Update(updatePost);
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);

            //Assert
            Assert.Equal("The post name cannot be longer than 255 characters. (Parameter 'PostName')", exception.Message);

            MockPostRepository.MockVerifyUpdateAsync(updatePost, Times.Never());
        }
    }
}
