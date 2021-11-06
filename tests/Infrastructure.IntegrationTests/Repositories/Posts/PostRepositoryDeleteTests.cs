using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryDeleteTests
    {
        private readonly IPostRepository _postRepository;
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
            var newBlog = BlogBuilder.Create().Build();
            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithPostId(Guid.NewGuid())
                .WithName(LoremNET.Lorem.Words(10))
                .WithText(LoremNET.Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

            var post = await _postRepository.AddAsync(newPost);

            //Act
            await _postRepository.DeleteAsync(post);

            //Assert
            var deletedBlog = await _postRepository.GetByIdAsync(post.PostId);

            Assert.Null(deletedBlog);
        }


    }
}
