using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceGetAllTests : BlogServiceBase
    {
        public const int Skip = 0; //default pagination value
        public const int Take = 10; //default pagination value

        [Fact(DisplayName = "GetAll_BlogsWithoutPagingValues_UsesDefaultValueForSkipAndTake")]
        public async Task GetAll_BlogsWithoutPagingValues_UsesDefaultValueForSkipAndTake()
        {
            //Arrange
            await MockBlogRepository.MockSetupListAllAsync(new List<Blog>());

            //Act
            var blogs = await BlogService.GetAll();

            //Assert
            MockBlogRepository.MockVerifyListAllAsync(Skip, Take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_BlogsUsingInvalidSkipValue_UsesDefaultValueForSkip()")]
        public async Task GetAll_BlogsUsingInvalidSkipValue_UsesDefaultValueForSkip()
        {
            //Arrange
            await MockBlogRepository.MockSetupListAllAsync(new List<Blog>());

            //Act
            var blogs = await BlogService.GetAll(-10, Take);

            //Assert
            MockBlogRepository.MockVerifyListAllAsync(Skip, Take, Times.Once());
        }

        [Fact(DisplayName = "GetAll_BlogsUsingInvalidTakeValue_UsesDefaultValueForTake")]
        public async Task GetAll_BlogsUsingInvalidTakeValue_UsesDefaultValueForTake()
        {
            //Arrange
            await MockBlogRepository.MockSetupListAllAsync(new List<Blog>());

            //Act
            var blogs = await BlogService.GetAll(Skip, -10);

            //Assert
            MockBlogRepository.MockVerifyListAllAsync(Skip, Take, Times.Once());
        }

        [Theory(DisplayName = "GetAll_BlogsUsingPagination_IsCalledOnceUsingCorrectValues")]
        [InlineData(1,1)]
        [InlineData(10,100)]
        [InlineData(100,1000)]
        public async Task GetAll_BlogsUsingPagination_IsCalledOnceUsingCorrectValues(int skip, int take)
        {
            //Arrange
            await MockBlogRepository.MockSetupListAllAsync(new List<Blog>());

            //Act
            var blogs = await BlogService.GetAll(skip, take);

            //Assert
            MockBlogRepository.Verify(mock => mock.ListAllAsync(skip, take), Times.Once);
        }
    }
}