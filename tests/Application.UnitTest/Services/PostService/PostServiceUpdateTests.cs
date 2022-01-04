using Application.UnitTest.Builders;
using BlogWebApi.Application.Dto;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceUpdateTests : PostServiceBase
    {
        [Fact(DisplayName = "Update_Post_IsCalledOnce")]
        public async Task Update_Post_IsCalledOnce()
        {
            var postId = Guid.NewGuid();
            var postName = Lorem.Words(10);
            var text = Lorem.Sentence(10);
            var blogId = Guid.NewGuid();

            //Arrange
            var updatePost = new Post
            {
                PostId = postId,
                PostName = postName,
                Text = text,
                BlogId = blogId
            };

            var updateDto = new PostUpdateRequestDto
            {
                PostId = postId,
                PostName = postName,
                Text = text,
                BlogId = blogId
            };

            await MockPostRepository.MockSetupGetByIdAsync(updatePost);

            MockPostRepository.MockSetupUpdateAsync(updatePost);

            //Act
            await PostService.Update(updateDto);

            //Assert
            MockPostRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            MockPostRepository.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact(DisplayName = "Update_NonExistentPost_NotFoundException")]
        public async Task Update_NonExistentPost_IsCalledOnce()
        {
            //Arrange
            var updatePost = PostBuilder.Default();

            var updateDto = new PostUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            await MockPostRepository.MockSetupGetByIdAsync(null);
            MockPostRepository.MockSetupUpdateAsync(updatePost);

            //Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await PostService.Update(updateDto));

            //Assert
            Assert.Equal($"The PostId: {updateDto.PostId} does not exist.", exception.Message);

            MockPostRepository.MockVerifyUpdateAsync(updatePost, Times.Never());
        }

        [Fact(DisplayName = "Update_NullPost_ThrowsBadRequestException")]
        public async Task Update_NullPost_ThrowsBadRequestException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.Update(null);
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

            // Assert
            Assert.Equal("The post cannot be null.", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyPostId_ThrowsBadRequestException")]
        public async Task Update_EmptyPostId_ThrowsBadRequestException()
        {
            //Arrange
            var updateDto = new PostUpdateRequestDto
            {
                PostId = Guid.Empty,
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Update(updateDto));

            // Assert
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsBadRequestException(string name)
        {
            //Arrange
            var updateDto = new PostUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                PostName = name,
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Update(updateDto));

            // Assert
            Assert.Equal("The post name cannot be null or empty.", exception.Message);
        }

        [Theory(DisplayName = "Update_WithEmptyOrNullText_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullText_ThrowsBadRequestException(string text)
        {
            //Arrange
            var updateDto = new PostUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = text,
                BlogId = Guid.NewGuid()
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Update(updateDto));

            // Assert
            Assert.Equal("The post text cannot be null or empty.", exception.Message);
        }

        [Fact(DisplayName = "Update_EmptyBlogId_ThrowsBadRequestException")]
        public async Task Update_EmptyBlogId_ThrowsBadRequestException()
        {
            //Arrange
            var updateDto = new PostUpdateRequestDto
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10)
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Update(updateDto));

            // Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);
        }

        [Fact(DisplayName = "Update_PostUsingLongName_ThrowsBadRequestException")]
        public async Task Update_PostUsingLongName_ThrowsBadRequestException()
        {
            //Arrange
            var updateDto = new PostUpdateRequestDto
            {

                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(50),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Update(updateDto));

            //Assert
            Assert.Equal("The post name cannot be longer than 255 characters.", exception.Message);
        }
    }
}
