using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using LoremNET;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceUpdateTests : BlogServiceBase
    {
        [Fact(DisplayName = "Update_BlogAnExistingBlog_ReturnsNoError")]
        public async Task Update_BlogAnExistingBlog_ReturnsNoError()
        {
            //Arrange
            var updateBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdAsync(updateBlog);
            MockBlogRepository.MockSetupUpdateAsync();

            //Act
            await BlogService.Update(updateBlog);

            //Assert
            MockBlogRepository.MockVerifyUpdateAsync(Times.Once());
        }

        [Fact(DisplayName = "Update_NonExistentBlog_ThrowsArgumentException")]
        public async Task Update_NonExistentBlog_ThrowsArgumentException()
        {
            //Arrange
            var updateBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupGetByIdAsync(null);
            MockBlogRepository.MockSetupUpdateAsync();

            // Act
            async Task Act() => await BlogService.Update(updateBlog);
            var exception = await Assert.ThrowsAsync<ArgumentException>(Act);

            // Assert
            Assert.Equal($"The blog with {updateBlog.BlogId} does not exist. (Parameter 'BlogId')", exception.Message);

            MockBlogRepository.MockVerifyUpdateAsync(Times.Never());
        }

        [Fact(DisplayName = "Update_NullBlog_ThrowsArgumentNullException")]
        public async Task Update_NullBlog_ThrowsArgumentNullException()
        {
            //Arrange

            // Act
            async Task Act() => await BlogService.Update(null);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("Value cannot be null. (Parameter 'blog')", exception.Message);

        }

        [Fact(DisplayName = "Update_WithEmptyBlogId_ThrowsArgumentNullException")]
        public async Task Update_WithEmptyBlogId_ThrowsArgumentNullException()
        {
            //Arrange
            var blog = BlogBuilder.Create().WithEmptyBlogId().Build();

            // Act
            async Task Act() => await BlogService.Update(blog);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The blogId cannot be empty Guid. (Parameter 'blog')", exception.Message);

        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsArgumentNullException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsArgumentNullException(string name)
        {
            //Arrange
            var blog = BlogBuilder.Default();
            blog.BlogName = name;

            // Act
            async Task Act() => await BlogService.Update(blog);
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);

            // Assert
            Assert.Equal("The blog name cannot be null or empty. (Parameter 'blog')", exception.Message);

        }

        [Fact(DisplayName = "Update_BlogWithLongBlogName_ThrowsNullArgumentException")]
        public async Task Update_BlogWithLongBlogName_ThrowsNullArgumentException()
        {
            //Arrange
            var blog = BlogBuilder.Default();
            blog.BlogName = Lorem.Words(255);

            //Act
            async Task Act() => await BlogService.Update(blog);
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);

            //Assert
            Assert.Equal("The blog name cannot be longer than 255 characters. (Parameter 'BlogName')",
                exception.Message);

            MockBlogRepository.MockVerifyAddAsync(blog, Times.Never());
        }
    }
}