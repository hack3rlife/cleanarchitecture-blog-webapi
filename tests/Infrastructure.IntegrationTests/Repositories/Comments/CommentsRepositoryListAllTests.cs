﻿using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Comments
{
    [Collection("DatabaseCollectionFixture")]
   public class CommentsRepositoryListAllTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly BlogRepository _blogRepository;
        private readonly PostRepository _postRepository;
        private readonly CommentRepository _commentRepository;

        public CommentsRepositoryListAllTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _commentRepository = new CommentRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "CommentRepository_ListAllComment_Success")]
        public async Task CommentRepository_ListAllComment_Success()
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

            for (var i = 0; i < 10; i++)
            {

                var newComment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentName = Lorem.Sentence(5),
                    Email = Lorem.Email(),
                    PostId = post.PostId,
                };
                _ = await _commentRepository.AddAsync(newComment);
            }

            var skip = 1;
            var take = 5;

            //Act
            var comments = await _commentRepository.ListAllAsync(skip, take);
            
            //Assert
            Assert.NotNull(comments);
            Assert.True(comments.Count == take);
        }
    }
}
