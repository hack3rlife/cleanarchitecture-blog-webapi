using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blog
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryGetByTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;

        private readonly DatabaseFixture _fixture;

        public BlogRepositoryGetByTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(_fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);

        }

        [Fact(DisplayName = "BlogRepository_ByIdAsync_ShouldSucceed")]
        public async Task BlogRepository_ByIdAsync_ShouldSucceed()
        {
            // Arrange
            var guid = Guid.NewGuid();

            var blog = BlogBuilder.Create()
                .WithId(guid)
                .Build();

            await _blogRepository.AddAsync(blog);

            // Act
            var blogById = await _blogRepository.GetByIdAsync(guid);

            //Assert
            Assert.NotNull(blogById);
            Assert.Equal(guid, blogById.BlogId);
        }

        [Fact(DisplayName = "BlogRepository_ByIdNonExistent_ReturnsNull")]
        public async Task BlogRepository_ByIdNonExistent_ReturnsNull()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var blogById = await _blogRepository.GetByIdAsync(guid);

            //Assert
            Assert.Null(blogById);
        }

        [Fact(Skip = "BlogRepository_GetPostsByBlogId_Success")]
        public async Task BlogRepository_GetPostsByBlogId_Success()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();
            var expectedBlog = await _blogRepository.AddAsync(newBlog);

            for (var i = 0; i < 20; i++)
            {

                var newPost = PostBuilder.Create()
                    .WithPostId(Guid.NewGuid())
                    .WithName(LoremNET.Lorem.Words(10))
                    .WithText(LoremNET.Lorem.Sentence(100))
                    .WithBlogId(expectedBlog.BlogId)
                    .Build();
                _ = await _postRepository.AddAsync(newPost);
            }

            //Act
            var blog = await _blogRepository.GetByIdWithPostsAsync(expectedBlog.BlogId);

            //Assert
            Assert.NotNull(blog);
            Assert.Equal(expectedBlog.BlogId, blog.BlogId);
            Assert.True(blog.Post.Count == 10);
        }

        [Fact(Skip = "BlogRepository_GetPostsByBlogIdWithPaging_ReturnsPaginatedResults")]
        public async Task BlogRepository_GetPostsByBlogIdWithPaging_ReturnsPaginatedResults()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();
            var expectedBlog = await _blogRepository.AddAsync(newBlog);

            for (var i = 0; i < 10; i++)
            {

                var newPost = PostBuilder.Create()
                    .WithPostId(Guid.NewGuid())
                    .WithName(LoremNET.Lorem.Words(10))
                    .WithText(LoremNET.Lorem.Sentence(100))
                    .WithBlogId(expectedBlog.BlogId)
                    .Build();
                _ = await _postRepository.AddAsync(newPost);
            }

            //Act
            var blog = await _blogRepository.GetByIdWithPostsAsync(expectedBlog.BlogId, 0,5);

            //Assert
            Assert.NotNull(blog);
            Assert.Equal(expectedBlog.BlogId, blog.BlogId);
            Assert.True(blog.Post.Count == 5);
        }
    }
}