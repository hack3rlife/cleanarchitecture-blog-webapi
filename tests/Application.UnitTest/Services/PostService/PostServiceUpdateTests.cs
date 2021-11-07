using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using BlogWebApi.Domain;
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

        [Fact(DisplayName = "Update_NullPost_ThrowsArgumentNullException")]
        public async Task Update_NullPost_ThrowsArgumentNullException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.Update(null);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The post cannot be null. (Parameter 'post')", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyPostId_ThrowsArgumentNullException")]
        public async Task Update_EmptyPostId_ThrowsArgumentNullException()
        {
            //Arrange
            var post = PostBuilder.WithoutPostId();

            //Act
            async Task Act() => await PostService.Update(post);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'post')", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsArgumentNullException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsArgumentNullException(string name)
        {
            //Arrange
            var post = PostBuilder.Default();
            post.PostName = name;

            //Act
            async Task Act() => await PostService.Update(post);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The post name cannot be null or empty. (Parameter 'post')", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullText_ThrowsArgumentNullException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullText_ThrowsArgumentNullException(string text)
        {
            //Arrange
            var post = PostBuilder.Default();
            post.Text = text;

            //Act
            async Task Act() => await PostService.Update(post);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The post text cannot be null or empty. (Parameter 'post')", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyBlogId_ThrowsArgumentNullException")]
        public async Task Update_EmptyBlogId_ThrowsArgumentNullException()
        {
            //Arrange
            var post = PostBuilder.WithoutBlogId();

            //Act
            async Task Act() => await PostService.Update(post);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The blogId cannot be empty Guid. (Parameter 'post')", exception.Message);
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
