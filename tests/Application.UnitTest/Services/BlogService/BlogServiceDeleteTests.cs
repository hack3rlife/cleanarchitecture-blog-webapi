using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceDeleteTests : BlogServiceBase
    {
        [Fact(DisplayName = "Delete_ExistingBlog_IsCalledOnce")]
        public async Task Delete_ExistingBlog_IsCalledOnce()
        {
            // Arrange
            var blog = new Blog
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };

            await MockBlogRepository.MockSetupGetByIdAsync(blog);

            MockBlogRepository.MockSetupDeleteAsync();

            //Act
            await BlogService.Delete(Guid.NewGuid());

            //Assert
            MockBlogRepository.MockVerifyDeleteAsync(Times.Once());
        }

        [Fact(DisplayName = "Delete_BlogWithEmptyGuid_ThrowsBadRequestException")]
        public async Task Delete_BlogWithEmptyGuid_ThrowsBadRequestException()
        {
            // Arrange
            MockBlogRepository.MockSetupDeleteAsync();

            // Act
           var exception = await Assert.ThrowsAsync<BadRequestException>(() => BlogService.Delete(Guid.Empty));

            // Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);

            MockBlogRepository.MockVerifyDeleteAsync(Times.Never());
        }

        [Fact(DisplayName = "Delete_BlogWithInvalidGuid_NotFoundException")]
        public async Task Delete_NonExistentBlog_NotFoundException()
        {
            // Arrange
            var blogId = Guid.NewGuid();

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await BlogService.Delete(blogId));

            // Assert
            Assert.Equal($"The blogId: {blogId} does not exist.", exception.Message);

            MockBlogRepository.MockVerifyDeleteAsync(Times.Never());
        }
    }
}