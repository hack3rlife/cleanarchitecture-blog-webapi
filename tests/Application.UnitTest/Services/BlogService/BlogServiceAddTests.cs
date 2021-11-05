using System;
using System.Threading.Tasks;
using Application.UnitTest.Builders;
using LoremNET;
using Moq;
using Xunit;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceAddTests : BlogServiceBase
    {
        [Fact(DisplayName = "Add_Blog_IsCalledOnce")]
        public async Task Add_Blog_IsCalledOnce()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();

            await MockBlogRepository.MockSetupAddAsync(newBlog);

            //Act
            var blog = await BlogService.Add(newBlog);

            //Assert
            Assert.NotNull(blog);
            MockBlogRepository.MockVerifyAddAsync(newBlog, Times.Once());
        }

        [Fact(DisplayName = "Add_BlogWithEmptyBlogName_ThrowsNullArgumentException")]
        public async Task Add_BlogWithEmptyBlogName_ThrowsNullArgumentException()
        {
            //Arrange
            var newBlog = BlogBuilder
                .Create()
                .WithEmptyName()
                .Build();

            //Act
            async Task Act() => await BlogService.Add(newBlog);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The blog name cannot be null or empty. (Parameter 'blog')", exception.Message);

            MockBlogRepository.MockVerifyAddAsync(newBlog, Times.Never());
        }

        [Fact(DisplayName = "Add_BlogWithBlankBlogName_ThrowsNullArgumentException")]
        public async Task Add_BlogWithBlankBlogName_ThrowsNullArgumentException()
        {
            //Arrange
            var newBlog = BlogBuilder
                .Create()
                .WithName(" ")
                .Build();

            //Act
            async Task Act() => await BlogService.Add(newBlog);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("The blog name cannot be null or empty. (Parameter 'blog')", exception.Message);

            MockBlogRepository.MockVerifyAddAsync(newBlog, Times.Never());
        }

        [Fact(DisplayName = "Add_NullBlog_ThrowsNullArgumentException")]
        public async Task Add_NullBlog_ThrowsNullArgumentException()
        {
            //Arrange && Act
            async Task Act() => await BlogService.Add(null);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Act);
            Assert.Equal("Value cannot be null. (Parameter 'blog')", exception.Message);

            MockBlogRepository.MockVerifyAddAsync(null, Times.Never());
        }

        [Fact(DisplayName = "Add_BlogWithLongBlogName_ThrowsNullArgumentException")]
        public async Task Add_BlogWithLongBlogName_ThrowsNullArgumentException()
        {
            //Arrange
            var newBlog = BlogBuilder
                .Create()
                .WithName(Lorem.Words(50))
                .Build();

            //Act
            async Task Act() => await BlogService.Add(newBlog);

            //Assert
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(Act);
            Assert.Equal("The blog name cannot be longer than 255 characters. (Parameter 'BlogName')",
                exception.Message);

            MockBlogRepository.MockVerifyAddAsync(newBlog, Times.Never());
        }
    }
}