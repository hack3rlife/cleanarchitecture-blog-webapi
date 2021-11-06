using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using LoremNET;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Comments
{
    [Collection("DatabaseCollectionFixture")]
   public class CommentsRepositoryAddTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentsRepositoryAddTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _commentRepository = new CommentRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "CommentRepository_AddComment_Success")]
        public async Task CommentRepository_AddComment_Success()
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

            var newComment = new Comment
            {
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email(),
                PostId = post.PostId,
            };

            //Act
            var comment = await _commentRepository.AddAsync(newComment);
            
            //Assert
            Assert.NotNull(comment);
            Assert.Equal(newComment.CommentId, comment.CommentId);
            Assert.Equal(newComment.CommentName, comment.CommentName);
            Assert.Equal(newComment.Email, comment.Email);
            Assert.Equal(newComment.PostId, comment.PostId);
        }
    }
}
