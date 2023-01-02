using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryDeleteTests
    {
        private readonly PostRepository _postRepository;
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public PostRepositoryDeleteTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _postRepository  = new PostRepository(fixture.BlogDbContext);
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "PostRepository_DeletePost_Success")]
        public async Task PostRepository_DeletePost_Success()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };

            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = new Post
            {
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(100),
                BlogId = blog.BlogId,
            };

            var post = await _postRepository.AddAsync(newPost);

            //Act
            await _postRepository.DeleteAsync(post);

            //Assert
            var deletedBlog = await _postRepository.GetByIdAsync(post.PostId);

            Assert.Null(deletedBlog);
        }


    }
}
