using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceGetByTests : PostServiceBase
    {
        private readonly Post returnPost;

        public PostServiceGetByTests()
        {
            returnPost = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),
                BlogId = Guid.NewGuid()
            };
        }

        [Fact(DisplayName = "GetBy_PostId_IsCalledOnce")]
        public async Task GetBy_PostId_IsCalledOnce()
        {
            //Arrange
            await MockPostRepository.MockSetupGetByIdAsync(returnPost);

            //Act
            var postById = await PostService.GetBy(returnPost.PostId);

            //Assert
            Assert.True(postById.GetType() == typeof(PostDetailsResponseDto));

            MockPostRepository.MockVerifyGetByIdAsync(returnPost, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostId_ThrowsBadRequestException")]
        public async Task GetBy_PostId_ThrowsBadRequestException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.GetBy(Guid.Empty);

            //Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingCommentsNoPagingValues_UsesDefaultValuesForSkipAndTake")]
        public async Task GetBy_PostIdIncludingCommentsNoPagingValues_UsesDefaultValuesForSkipAndTake()
        {
            //Arrange
            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, Skip, Take);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId);

            //Assert 
            Assert.NotNull(post); Assert.True(post.GetType() == typeof(PostDetailsResponseDto));

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, Skip, Take, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingCommentsWithInvalidSkipValue_UsesDefaultValuesForSkip")]
        public async Task GetBy_PostIdIncludingCommentsWithInvalidSkipValue_UsesDefaultValuesForSkip()
        {
            //Arrange
            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, Skip, 5);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId,-1, 5);

            //Assert 
            Assert.NotNull(post);

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, Skip, 5, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingCommentsWithInvalidTakeValue_UsesDefaultValuesForTake")]
        public async Task GetBy_PostIdIncludingCommentsWithInvalidTakeValue_UsesDefaultValuesForTake()
        {
            //Arrange
            var take = 0;

            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, Skip, Take);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId, Skip, take);

            //Assert 
            Assert.NotNull(post);

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, Skip, Take, Times.Once());
        }

        [Theory(DisplayName = "GetBy_PostIdIncludingComments_IsCalledOnce")]
        [InlineData(0,10)]
        [InlineData(10,100)]
        [InlineData(100,1000)]
        public async Task GetBy_PostIdIncludingComments_IsCalledOnce(int skip, int take)
        {
            //Arrange
            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, skip, take);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId, skip, take);

            //Assert 
            Assert.NotNull(post);

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, skip, take, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingComments_ThrowsBadRequestException")]
        public async Task GetBy_PostIdIncludingComments_ThrowsBadRequestException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.GetCommentsBy(Guid.Empty);

            //Assert 
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);
            Assert.Equal("The postId cannot be empty Guid.", exception.Message);
        }
    }
}