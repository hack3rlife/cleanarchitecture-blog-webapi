using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Moq;
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
            Assert.Equal(expectedBlog, actualBlog);
            MockBlogRepository.MockVerifyGetByIdAsync(expectedBlog, Times.Once());
        }

        [Fact(DisplayName = "GetBy_EmptyGuid_ThrowsArgumentNullException")]
        public async Task GetBy_EmptyGuid_ThrowsArgumentNullException()
        {
            //Arrange
            var expectedBlog = BlogBuilder
                .Create()
                .WithEmptyBlogId()
                .Build();

            //Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => BlogService.GetBy(expectedBlog.BlogId));

            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, 0, 10, Times.Never());
        }

        [Fact(DisplayName = "GetBy_EmptyGuid_ThrowsArgumentNullException")]
        public async Task GetBy_PostIdWithEmptyBlogId_ThrowsArgumentNullException()
        {
            //Arrange
            var blogId = Guid.Empty;

            //Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => BlogService.GetPostsBy(blogId));

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
            Assert.Equal(expectedBlog, actualBlog);
            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, Times.Once());
        }

        [Fact(DisplayName = "GetBy_BlogIdWithPostsUsingInvalidSkipValue_UsesDefaultValueForSkip")]
        public async Task GetBy_BlogIdWithPostsUsingInvalidSkipValue_UsesDefaultValueForSkip()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId, -1, 2);

            //Assert
            Assert.Equal(expectedBlog, actualBlog);
            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, 0, 2, Times.Once());
        }

        [Fact(DisplayName = "GetBy_BlogIdWithPostsUsingInvalidTakeValue_UsesDefaultValueForTake")]
        public async Task GetBy_BlogIdWithPostsUsingInvalidTakeValue_UsesDefaultValueForTake()
        {
            //Arrange
            var expectedBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdWithPostsAsync(expectedBlog.BlogId, expectedBlog);

            //Act
            var actualBlog = await BlogService.GetPostsBy(expectedBlog.BlogId, 1, -1);

            //Assert
            Assert.Equal(expectedBlog, actualBlog);
            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, 1, 10, Times.Once());
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
            Assert.Equal(expectedBlog, actualBlog);
            MockBlogRepository.MockVerifyGetByIdWithPostsAsync(expectedBlog.BlogId, skip, take, Times.Once());
        }
    }
}