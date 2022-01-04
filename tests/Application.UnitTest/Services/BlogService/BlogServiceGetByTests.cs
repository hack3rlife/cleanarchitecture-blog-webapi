using Application.UnitTest.Builders;
using Moq;
using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Exceptions;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceGetByTests : BlogServiceBase
    {
        [Fact(DisplayName = "GetBy_BlogId_IsCalledOnce")]
        public async Task GetBy_BlogId_IsCalledOnce()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdAsync(expectedBlog);

            //Act
            var actualBlog = await BlogService.GetBy(expectedBlog.BlogId);

            //Assert
            Assert.Equal(expectedBlog.BlogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog.BlogName, actualBlog.BlogName);
            
            MockBlogRepository.MockVerifyGetByIdAsync(expectedBlog, Times.Once());
        }

        [Fact(DisplayName = "GetBy_EmptyGuid_ThrowsBadRequestException")]
        public async Task GetBy_EmptyGuid_ThrowsBadRequestException()
        {
            //Arrange
            var blogId = Guid.Empty;

            //Act
           var exception =  await Assert.ThrowsAsync<BadRequestException>(() => BlogService.GetBy(blogId));

            //Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(blogId, 0, 10, Times.Never());
        }

        [Fact(DisplayName = "GetBy_PostIdWithEmptyBlogId_ThrowsBadRequestException")]
        public async Task GetBy_PostIdWithEmptyBlogId_ThrowsBadRequestException()
        {
            //Arrange
            var blogId = Guid.Empty;

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => BlogService.GetPostsBy(blogId));

            //Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(blogId, 0, 10, Times.Never());
        }

        [Fact(DisplayName = "GetBy_BlogIdWithPostsNoPagingValues_UsesDefaultValueForSkipAndTake")]
        public async Task GetBy_BlogIdWithPostsNoPagingValues_UsesDefaultValueForSkipAndTake()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId);

            //Assert
            Assert.Equal(expectedBlog.BlogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog.BlogName, actualBlog.BlogName);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, Times.Once());
        }

        [Fact(DisplayName = "GetBy_BlogIdWithPostsUsingInvalidSkipValue_UsesDefaultValueForSkip")]
        public async Task GetBy_BlogIdWithPostsUsingInvalidSkipValue_UsesDefaultValueForSkip()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId);

            //Assert
            Assert.Equal(expectedBlog.BlogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog.BlogName, actualBlog.BlogName);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, 0, 10, Times.Once());
        }

        [Fact(DisplayName = "GetBy_BlogIdWithPostsUsingInvalidTakeValue_UsesDefaultValueForTake")]
        public async Task GetBy_BlogIdWithPostsUsingInvalidTakeValue_UsesDefaultValueForTake()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();
            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId);

            //Assert
            Assert.Equal(expectedBlog.BlogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog.BlogName, actualBlog.BlogName);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, 0, 10, Times.Once());
        }

        [Theory(DisplayName = "GetBy_BlogIdWithPosts_IsCalledOnce")]
        [InlineData(1, 1)]
        [InlineData(10, 100)]
        [InlineData(1000, 100)]
        public async Task GetBy_BlogIdWithPosts_IsCalledOnce(int skip, int take)
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId, skip, take);

            //Assert
            Assert.Equal(expectedBlog.BlogId, actualBlog.BlogId);
            Assert.Equal(expectedBlog.BlogName, actualBlog.BlogName);

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, skip, take, Times.Once());
        }
    }
}