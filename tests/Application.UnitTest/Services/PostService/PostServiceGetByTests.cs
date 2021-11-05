using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceGetByTests : PostServiceBase
    {
        [Fact(DisplayName = "GetBy_PostId_IsCalledOnce")]
        public async Task GetBy_PostId_IsCalledOnce()
        {
            //Arrange
            var thePost = PostBuilder.Default();
            await MockPostRepository.MockSetupGetByIdAsync(thePost);

            //Act
            var postById = await PostService.GetBy(thePost.PostId);

            //Assert
            Assert.NotNull(postById);
            MockPostRepository.MockVerifyGetByIdAsync(thePost, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostId_ThrowsArgumentNullException")]
        public async Task GetBy_PostId_ThrowsArgumentNullException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.GetBy(Guid.Empty);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'postId')", exception.Message);
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingCommentsNoPagingValues_UsesDefaultValuesForSkipAndTake")]
        public async Task GetBy_PostIdIncludingCommentsNoPagingValues_UsesDefaultValuesForSkipAndTake()
        {
            //Arrange
            var returnPost = PostBuilder.Default();

            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, Skip, Take);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId);

            //Assert 
            Assert.NotNull(post);

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, Skip, Take, Times.Once());

        }

        [Fact(DisplayName = "GetBy_PostIdIncludingCommentsWithInvalidSkipValue_UsesDefaultValuesForSkip")]
        public async Task GetBy_PostIdIncludingCommentsWithInvalidSkipValue_UsesDefaultValuesForSkip()
        {
            //Arrange
            var returnPost = PostBuilder.Default();

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

            var returnPost = PostBuilder.Default();

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
            var returnPost = PostBuilder.Default();
            await MockPostRepository.MockSetupGetByIdWithCommentsTask(returnPost, skip, take);

            //Act
            var post = await PostService.GetCommentsBy(returnPost.PostId, skip, take);

            //Assert 
            Assert.NotNull(post);

            MockPostRepository.MockVerifyGetByIdWithPostsAsync(returnPost.PostId, skip, take, Times.Once());
        }

        [Fact(DisplayName = "GetBy_PostIdIncludingComments_ThrowsArgumentNullException")]
        public async Task GetBy_PostIdIncludingComments_ThrowsArgumentNullException()
        {
            //Arrange

            //Act
            async Task Act() => await PostService.GetCommentsBy(Guid.Empty);

            //Assert 
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The postId cannot be empty Guid. (Parameter 'postId')", exception.Message);
        }
    }
}