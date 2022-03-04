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
    public class BlogServiceAddTests : BlogServiceBase
    {
        [Fact(DisplayName = "Add_Blog_IsCalledOnce")]
        public async Task Add_Blog_IsCalledOnce()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var name = Lorem.Words(10);

            var newBlog = new Blog
            {
                BlogName = name,
                BlogId = guid
            };

            var newDto = new BlogAddRequestDto
            {
                BlogId = guid,
                BlogName = name
            };

           await MockBlogRepository.MockSetupAddAsync(newBlog);

            //Act
            var blog = await BlogService.Add(newDto);

            //Assert
            Assert.NotNull(blog);

            Assert.Equal(newDto.BlogId, blog.BlogId);
            Assert.Equal(newDto.BlogName, blog.BlogName);

            MockBlogRepository.MockVerifyAddAsync(Times.Once());
        }

        [Theory(DisplayName = "Add_BlogWithEmptyOrNullBlogName_ThrowsBadRequestException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Add_BlogWithEmptyOrNullBlogName_ThrowsBadRequestException(string name)
        {
            //Arrange
            var newBlog = new BlogAddRequestDto
            {
                BlogName = name
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await BlogService.Add(newBlog));

            //Assert
            Assert.Equal("The blog name cannot be null or empty.", exception.Message);
        }

        [Fact(DisplayName = "Add_NullBlog_ThrowsBadRequestException")]
        public async Task Add_NullBlog_ThrowsBadRequestException()
        {
            //Arrange && Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await BlogService.Add(null));

            //Assert
            Assert.Equal("Blog information cannot be null.", exception.Message);
        }

        [Fact(DisplayName = "Add_BlogWithLongBlogName_ThrowsBadRequestException")]
        public async Task Add_BlogWithLongBlogName_ThrowsBadRequestException()
        {
            //Arrange
            var newBlog = new BlogAddRequestDto
            {
                BlogName = Lorem.Words(50)
            };

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await BlogService.Add(newBlog));

            //Assert
            Assert.Equal("The blog name cannot be longer than 255 characters.", exception.Message);
        }
    }
}