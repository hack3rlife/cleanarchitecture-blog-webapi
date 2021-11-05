using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceGetAllTests : PostServiceBase
    {
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

        [Fact(DisplayName = "GetAll_PostsUsingInvalidSkipValue_RespectsSkipDefaultValue")]
        public async Task GetAll_PostsUsingInvalidSkipValue_RespectsSkipDefaultValue()
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act
            const int skip = 0;
            const int take = 2;

            var posts = await PostService.GetAll(skip, take);

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(Skip, take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_PostsUsingInvalidTakeValue_RespectsTakeDefaultValue")]
        public async Task GetAll_PostsUsingInvalidTakeValue_RespectsTakeDefaultValue()
        {
            //Arrange
            var returnPosts = new List<Post>();

            await MockPostRepository.MockSetupListAllAsync(returnPosts);

            //Act
            const int skip = 5;
            const int take = 0;

            var posts = await PostService.GetAll(skip, take);

            //Assert
            Assert.NotNull(posts);

            MockPostRepository.MockVerifyListAllAsync(Skip, Take, Times.Never());
        }
    }
}
