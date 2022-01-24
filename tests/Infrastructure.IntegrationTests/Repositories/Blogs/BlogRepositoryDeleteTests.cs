using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryDeleteTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public BlogRepositoryDeleteTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(_fixture.BlogDbContext);
        }

        [Fact(DisplayName = "BlogRepository_DeleteAsync_Success")]
        public async Task BlogRepository_DeleteAsync_Success()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };

            var blog = await _blogRepository.AddAsync(newBlog);

            //Act
            await _blogRepository.DeleteAsync(blog);

            //Assert
            var deletedBlog = await _blogRepository.GetByIdAsync(blog.BlogId);

            Assert.Null(deletedBlog);
        }
    }
}