using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceDeleteTests : BlogServiceBase
    {
        [Fact(DisplayName = "Delete_ExistingBlog_IsCalledOnce")]
        public async Task Delete_ExistingBlog_IsCalledOnce()
        {
            // Arrange
            await MockBlogRepository.MockSetupGetByIdAsync(BlogBuilder.Default());

            MockBlogRepository.MockSetupDeleteAsync();

            //Act
            await BlogService.Delete(Guid.NewGuid());

            //Assert
            MockBlogRepository.MockVerifyDeleteAsync(Times.Once());
        }

        [Fact(DisplayName = "Delete_BlogWithEmptyGuid_ThrowsArgumentNullException")]
        public async Task Delete_BlogWithEmptyGuid_ThrowsArgumentNullException()
        {
            // Arrange
            MockBlogRepository.MockSetupDeleteAsync();

            // Act
           var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => BlogService.Delete(Guid.Empty));

            // Assert
            Assert.Equal("The blogId cannot be empty Guid. (Parameter 'blogId')", exception.Message);

            MockBlogRepository.MockVerifyDeleteAsync(Times.Never());
        }

        [Fact(DisplayName = "Delete_BlogWithInvalidGuid_ThrowsArgumentException")]
        public async Task Delete_NonExistentBlog_ThrowsArgumentException()
        {
            // Arrange
            var blogId = Guid.NewGuid();

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await BlogService.Delete(blogId));

            // Assert
            Assert.Equal($"The blogId {blogId} does not exist. (Parameter 'blogId')", exception.Message);

            MockBlogRepository.MockVerifyDeleteAsync(Times.Never());
        }
    }
}