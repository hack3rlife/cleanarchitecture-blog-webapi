using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryListAllTests
    {
        private readonly PostRepository _postRepository;
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public PostRepositoryListAllTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "PostRepository_ListAllPost_Success")]
        public async Task PostRepository_ListAllPost_Success()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };

            var blog = await _blogRepository.AddAsync(newBlog);

            for (var i = 0; i < 10; i++)
            {

                var newPost = new Post
                {
                    PostId = Guid.NewGuid(),
                    PostName = Lorem.Words(10),
                    Text = Lorem.Sentence(100),
                    BlogId = blog.BlogId,
                };

                _ = await _postRepository.AddAsync(newPost);
            }

            //Act
            var posts = await _postRepository.ListAllAsync(1,5);

            //Assert
            Assert.NotNull(posts);
            Assert.True(posts.Count == 5);
        }

        [Fact(DisplayName = "PostRepository_ListAllPostWithInvalidPaginationValues_ShouldUseDefaultPaginationValues")]
        public async Task PostRepository_ListAllPostWithInvalidPaginationValues_ShouldUseDefaultPaginationValues()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };

            var blog = await _blogRepository.AddAsync(newBlog);

            for (var i = 0; i < 15; i++)
            {

                var newPost = new Post
                {
                    PostId = Guid.NewGuid(),
                    PostName = Lorem.Words(10),
                    Text = Lorem.Sentence(100),
                    BlogId = blog.BlogId,
                };

                _ = await _postRepository.AddAsync(newPost);
            }

            //Act
            var posts = await _postRepository.ListAllAsync(-1, 0);

            //Assert
            Assert.NotNull(posts);
            Assert.True(posts.Count == 10);
        }
    }
}