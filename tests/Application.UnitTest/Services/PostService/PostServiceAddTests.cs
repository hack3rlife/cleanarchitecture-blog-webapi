using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceAddTests : PostServiceBase
    {
        [Fact(DisplayName = "Add_PostWithValidValues_IsCalledOnce")]
        public async Task Add_PostWithValidValues_IsCalledOnce()
        {
            //Arrange
            var newPost = PostBuilder.Default();

           await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            var post = await PostService.Add(newPost);

            //Assert
            Assert.NotNull(post);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Once);
        }

        [Fact(DisplayName = "Add_NullPost_ThrowsArgumentNullException")]
        public async Task Add_NullPost_ThrowsArgumentNullException()
        {
            //Arrange
           
            //Act
            async Task Act() => await PostService.Add(null);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The post cannot be null. (Parameter 'post')", exception.Message);
        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullName_ThrowsArgumentNullException")]
        public async Task Add_PostWithEmptyName_ThrowsArgumentNullException()
        {
            //Arrange
            var newPost = PostBuilder.WithoutName();

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            async Task Act() => await PostService.Add(newPost);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The post name cannot be null or empty. (Parameter 'PostName')", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);
        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullText_ThrowsArgumentNullException")]
        public async Task Add_PostWithEmptyOrNullText_ThrowsArgumentNullException()
        {
            //Arrange
            var newPost = PostBuilder.WithoutText();

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            async Task Act() => await PostService.Add(newPost);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The post text cannot be null or empty. (Parameter 'Text')", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);

        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullBlogId_ThrowsArgumentNullException")]
        public async Task Add_PostWithEmptyOrNullBlogId_ThrowsArgumentNullException()
        {
            //Arrange
            var newPost = PostBuilder.WithoutBlogId();

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            async Task Act() => await PostService.Add(newPost);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The blogId cannot be empty Guid. (Parameter 'BlogId')", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);

        }

        [Fact(DisplayName = "Add_PostWithLongName_ThrowsArgumentNullException")]
        public async Task Add_PostWithEmptyOrNullBlogId_ThrowsArgumentOutOfRangeException()
        {
            //Arrange
            var newPost = PostBuilder.WithLongName();

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            async Task Act() => await PostService.Add(newPost);

            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);

            //Assert
            Assert.Equal("The post name cannot be longer than 255 characters. (Parameter 'PostName')", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);

        }
    }
}
