﻿using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Comments
{
    [Collection("DatabaseCollectionFixture")]
   public class CommentsRepositoryGetByTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly BlogRepository _blogRepository;
        private readonly PostRepository _postRepository;
        private readonly CommentRepository _commentRepository;

        public CommentsRepositoryGetByTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _commentRepository = new CommentRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "CommentRepository_GetByComment_Success")]
        public async Task CommentRepository_GetByComment_Success()
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

            var newComment = new Comment
            {
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email(),
                PostId = post.PostId,
            };
            var comment = await _commentRepository.AddAsync(newComment);

            //Act
            var commentBy = await _commentRepository.GetByIdAsync(comment.CommentId);
            
            //Assert
            Assert.NotNull(comment);
            Assert.Equal(commentBy.CommentId, comment.CommentId);
            Assert.Equal(commentBy.CommentName, comment.CommentName);
            Assert.Equal(commentBy.Email, comment.Email);
            Assert.Equal(commentBy.PostId, comment.PostId);
        }
    }
}
