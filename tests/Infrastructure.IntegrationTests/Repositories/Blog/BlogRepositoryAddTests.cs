using System;
using System.Linq;
using System.Threading.Tasks;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using LoremNET;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blog
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryAddTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public BlogRepositoryAddTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(_fixture.BlogDbContext);
        }

        [Fact(DisplayName = "BlogRepository_AddBlog_Success")]
        public async Task BlogRepository_AddAsync_Success()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();

            //Act
            var blog = await _blogRepository.AddAsync(newBlog);

            //Assert
            Assert.NotNull(blog);
            Assert.Equal(newBlog.BlogId, blog.BlogId);
            Assert.Equal(newBlog.BlogName, blog.BlogName);
            Assert.False(blog.Post.Any());
        }

        [Fact(DisplayName = "BlogRepository_AddBlogWithoutGuid_Success")]
        public async Task BlogRepository_AddBlogWithoutGuid_Success()
        {
            //Arrange
            var newBlog = new BlogWebApi.Domain.Blog {BlogName = Lorem.Words(10)};

            //Act
            var blog = await _blogRepository.AddAsync(newBlog);

            //Assert
            Assert.NotNull(blog);
            Assert.False(blog.BlogId == Guid.Empty);
            Assert.Equal(newBlog.BlogName, blog.BlogName);
            Assert.False(blog.Post.Any());
        }
    }
}