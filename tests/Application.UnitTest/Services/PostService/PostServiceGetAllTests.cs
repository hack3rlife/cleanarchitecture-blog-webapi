using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceGetAllTests : PostServiceBase
    {
        public const int Skip = 0; //default pagination value
        public const int Take = 10; //default pagination value

        [Fact(DisplayName = "GetAll_PostsWithoutPaginationValues_UsesDefaultValueForSkipAndTake")]
        public async Task GetAll_PostsWithoutPaginationValues_UsesDefaultValueForSkipAndTake()
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act
            var posts = await PostService.GetAll();

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(Skip, Take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_PostsUsingInvalidSkipValue_RespectsSkipDefaultValue")]
        public async Task GetAll_PostsUsingInvalidSkipValue_RespectsSkipDefaultValue()
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act

            var posts = await PostService.GetAll(-10, Take);

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(Skip, Take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_PostsUsingInvalidTakeValue_RespectsTakeDefaultValue")]
        public async Task GetAll_PostsUsingInvalidTakeValue_RespectsTakeDefaultValue()
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act
            var posts = await PostService.GetAll(Skip, -10);

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(Skip, Take, Times.Never());
        }

        [Theory(DisplayName = "GetAll_PostsUsingPagination_IsCalledOnce")]
        [InlineData(1, 1)]
        [InlineData(10, 100)]
        [InlineData(100, 1000)]
        public async Task GetAll_PostsUsingPagination_IsCalledOnce(int skip, int take)
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act

            var posts = await PostService.GetAll(skip, take);

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(skip, take, Times.Once());
        }
    }
}
