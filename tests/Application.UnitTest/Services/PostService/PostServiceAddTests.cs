using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Exceptions;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceAddTests : PostServiceBase
    {
        [Fact(DisplayName = "Add_PostWithValidValues_IsCalledOnce")]
        public async Task Add_PostWithValidValues_IsCalledOnce()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var postName = Lorem.Words(10);
            var text = Lorem.Sentence(10);
            var blogId = Guid.NewGuid();

            var newDto = new PostAddRequestDto
            {
                PostId = postId,
                PostName = postName,
                Text = text,
                BlogId = blogId
            };

            await MockPostRepository.MockSetupAddAsync(new Post());

            //Act
            var newPost = await PostService.Add(newDto);

            //Assert
            Assert.NotNull(newPost);

            MockPostRepository.Verify(mock => mock.AddAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact(DisplayName = "Add_NullPost_ThrowsBadRequestException")]
        public async Task Add_NullPost_ThrowsBadRequestException()
        {
            //Arrange

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Add(null));

            //Assert
            Assert.Equal("The post cannot be null.", exception.Message);
        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullName_ThrowsBadRequestException")]
        public async Task Add_PostWithEmptyName_ThrowsBadRequestException()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var text = Lorem.Sentence(10);
            var blogId = Guid.NewGuid();

            var newPost = new Post
            {
                PostId = postId,
                Text = text,
                BlogId = blogId
            };

            var newDto = new PostAddRequestDto
            {
                PostId = postId,
                Text = text,
                BlogId = blogId
            };

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Add(newDto));

            //Assert
            Assert.Equal("The post name cannot be null or empty.", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);
        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullName_ThrowsBadRequestException")]
        public async Task Add_PostWithEmptyOrNullName_ThrowsBadRequestException()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var text = Lorem.Sentence(10);
            var blogId = Guid.NewGuid();
            var postName = Lorem.Sentence(50);

            var newPost = new Post
            {
                PostId = postId,
                Text = text,
                BlogId = blogId,
                PostName = postName
            };

            var newDto = new PostAddRequestDto
            {
                PostId = postId,
                Text = text,
                BlogId = blogId,
                PostName = postName
            };

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Add(newDto));

            //Assert
            Assert.Equal("The post name cannot be longer than 255 characters.", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);
        }

        [Fact(DisplayName = "Add_PostWithEmptyOrNullText_ThrowsBadRequestException")]
        public async Task Add_PostWithEmptyOrNullText_ThrowsBadRequestException()
        {
            //Arrange
            var postId = Guid.NewGuid();
            var postName = Lorem.Words(10);
            var blogId = Guid.NewGuid();

            var newPost = new Post
            {
                PostId = postId,
                PostName = postName,
                BlogId = blogId
            };

            var newDto = new PostAddRequestDto
            {
                PostId = postId,
                PostName = postName,
                BlogId = blogId
            };

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Add(newDto));

            //Assert
            Assert.Equal("The post text cannot be null or empty.", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);
        }

        [Theory(DisplayName = "Add_PostWithEmptyOrNullBlogId_ThrowsBadRequestException")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task Add_PostWithEmptyOrNullBlogId_ThrowsBadRequestException(Guid guid)
        {
            //Arrange
            var postId = Guid.NewGuid();
            var postName = Lorem.Words(10);
            var text = Lorem.Sentence(10);

            var newPost = new Post
            {
                PostId = postId,
                PostName = postName,
                Text = text,
                BlogId = guid
            };

            var newDto = new PostAddRequestDto
            {
                PostId = postId,
                PostName = postName,
                Text = text,
            };

            await MockPostRepository.MockSetupAddAsync(newPost);

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await PostService.Add(newDto));

            //Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);

            MockPostRepository.Verify(mock => mock.AddAsync(newPost), Times.Never);
        }
    }
}
