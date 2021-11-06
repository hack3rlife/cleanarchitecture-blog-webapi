using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using LoremNET;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryAddTests
    {
        private readonly IPostRepository _postRepository;
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
            var newBlog = BlogBuilder.Create().Build();
            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithPostId(Guid.NewGuid())
                .WithName(LoremNET.Lorem.Words(10))
                .WithText(LoremNET.Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

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
            var newBlog = BlogBuilder.Default();

            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithName(Lorem.Words(10))
                .WithText(Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

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
