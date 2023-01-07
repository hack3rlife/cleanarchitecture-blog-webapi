using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryAddTests
    {
        private readonly PostRepository _postRepository;
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public PostRepositoryAddTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _postRepository  = new PostRepository(fixture.BlogDbContext);
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "PostRepository_AddPost_Success")]
        public async Task PostRepository_AddPost_Success()
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

            //Act
            var post = await _postRepository.AddAsync(newPost);

            //Assert
            Assert.NotNull(post);
            Assert.Equal(newPost.PostId, post.PostId);
            Assert.Equal(newPost.PostName, post.PostName);
            Assert.Equal(newPost.Text, post.Text);
            Assert.Equal(newPost.BlogId, post.BlogId);
        }

        [Fact(DisplayName = "PostRepository_AddPostWithoutPostId_Success")]
        public async Task PostRepository_AddPostWithoutPostId_Success()
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

            //Act
            var post = await _postRepository.AddAsync(newPost);

            //Assert
            Assert.NotNull(post);
            Assert.Equal(newPost.PostId, post.PostId);
            Assert.Equal(newPost.PostName, post.PostName);
            Assert.Equal(newPost.Text, post.Text);
            Assert.Equal(newPost.BlogId, post.BlogId);
        }
    }
}
