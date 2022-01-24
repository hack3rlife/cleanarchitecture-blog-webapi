using BlogWebApi.Application.Dto;
using BlogWebApi.Application.Exceptions;
using BlogWebApi.Domain;
using LoremNET;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceUpdateTests : BlogServiceBase
    {
        private readonly Blog updateBlog;

        public BlogServiceUpdateTests()
        {
            updateBlog = new Blog
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };
        }

        [Fact(DisplayName = "Update_BlogAnExistingBlog_ReturnsNoError")]
        public async Task Update_BlogAnExistingBlog_ReturnsNoError()
        {
            //Arrange
            await MockBlogRepository.MockSetupGetByIdAsync(updateBlog);
            MockBlogRepository.MockSetupUpdateAsync();

            var updateRequestDto = new BlogUpdateRequestDto()
            {
                BlogId = updateBlog.BlogId,
                BlogName = updateBlog.BlogName
            };

            //Act
            await BlogService.Update(updateRequestDto);

            //Assert
            MockBlogRepository.MockVerifyUpdateAsync(Times.Once());
        }

        [Fact(DisplayName = "Update_NonExistentBlog_NotFoundException")]
        public async Task Update_NonExistentBlog_NotFoundException()
        {
            //Arrange
            await MockBlogRepository.MockSetupGetByIdAsync(null);
            MockBlogRepository.MockSetupUpdateAsync();

            var updateRequestDto = new BlogUpdateRequestDto
            {
                BlogId = updateBlog.BlogId,
                BlogName = updateBlog.BlogName
            };

            // Act
            async Task Act() => await BlogService.Update(updateRequestDto);
            var exception = await Assert.ThrowsAsync<NotFoundException>(Act);

            // Assert
            Assert.Equal($"The blog: {updateBlog.BlogId} does not exist.", exception.Message);

            MockBlogRepository.MockVerifyUpdateAsync(Times.Never());
        }

        [Fact(DisplayName = "Update_NullBlog_ThrowsBadRequestException")]
        public async Task Update_NullBlog_ThrowsBadRequestException()
        {
            //Arrange

            // Act
            async Task Act() => await BlogService.Update(null);
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

            // Assert
            Assert.Equal("The blog information cannot be null.", exception.Message);

        }

        [Fact(DisplayName = "Update_WithEmptyBlogId_ThrowsBadRequestException")]
        public async Task Update_WithEmptyBlogId_ThrowsBadRequestException()
        {
            //Arrange
            var updateRequestDto = new BlogUpdateRequestDto
            {
                BlogId = Guid.Empty,
                BlogName = updateBlog.BlogName
            };

            // Act
            async Task Act() => await BlogService.Update(updateRequestDto);
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

            // Assert
            Assert.Equal("The blogId cannot be empty Guid.", exception.Message);

        }

        [Theory(DisplayName = "Update_WithEmptyOrNullName_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Update_WithEmptyOrNullName_ThrowsBadRequestException(string name)
        {
            //Arrange
            var blogUpdateRequesteDto = new BlogUpdateRequestDto
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };
            blogUpdateRequesteDto.BlogName = name;

            // Act
            async Task Act() => await BlogService.Update(blogUpdateRequesteDto);
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

            // Assert
            Assert.Equal("The blog name cannot be null or empty.", exception.Message);

        }

        [Fact(DisplayName = "Update_BlogWithLongBlogName_ThrowsBadRequestException")]
        public async Task Update_BlogWithLongBlogName_ThrowsBadRequestException()
        {
            //Arrange
            var blogUpdateRequesteDto = new BlogUpdateRequestDto
            {
                BlogName = Lorem.Words(10),
                BlogId = Guid.NewGuid()
            };
            blogUpdateRequesteDto.BlogName = Lorem.Words(255);

            //Act
            async Task Act() => await BlogService.Update(blogUpdateRequesteDto);
            var exception = await Assert.ThrowsAsync<BadRequestException>(Act);

            //Assert
            Assert.Equal("The blog name cannot be longer than 255 characters.", exception.Message);

        }
    }
}