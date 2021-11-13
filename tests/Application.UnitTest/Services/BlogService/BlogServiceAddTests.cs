using BlogWebApi.Application.Dto;
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
            Assert.Equal(newDto.CreatedBy, blog.CreatedBy);

            MockBlogRepository.MockVerifyAddAsync(Times.Once());
        }

        [Theory(DisplayName = "Add_BlogWithEmptyOrNullBlogName_ThrowsNullArgumentException")]
        [InlineData("")]
        [InlineData(null)]
        public async Task Add_BlogWithEmptyOrNullBlogName_ThrowsNullArgumentException(string name)
        {
            //Arrange
            var newBlog = new BlogAddRequestDto
            {
                BlogName = name
            };

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await BlogService.Add(newBlog));

            //Assert
            Assert.Equal("The blog name cannot be null or empty. (Parameter 'blogAddRequestDto')", exception.Message);
        }

        [Fact(DisplayName = "Add_NullBlog_ThrowsNullArgumentException")]
        public async Task Add_NullBlog_ThrowsNullArgumentException()
        {
            //Arrange && Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () => await BlogService.Add(null));

            //Assert
            Assert.Equal("Value cannot be null. (Parameter 'blogAddRequestDto')", exception.Message);
        }

        [Fact(DisplayName = "Add_BlogWithLongBlogName_ThrowsNullArgumentException")]
        public async Task Add_BlogWithLongBlogName_ThrowsNullArgumentException()
        {
            //Arrange
            var newBlog = new BlogAddRequestDto
            {
                BlogName = Lorem.Words(50)
            };

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await BlogService.Add(newBlog));

            //Assert
            Assert.Equal("The blog name cannot be longer than 255 characters. (Parameter 'BlogName')", exception.Message);
        }
    }
}