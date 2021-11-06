﻿using System;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using LoremNET;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class PostRepositoryGetByTests
    {
        private readonly CommentRepository _commentRepository;
        private readonly PostRepository _postRepository;
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public PostRepositoryGetByTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _commentRepository = new CommentRepository(_fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "PostRepository_GetByPost_Success")]
        public async Task PostRepository_GetByPost_Success()
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
            var getByPost = await _postRepository.GetByIdAsync(post.PostId);

            //Assert
            Assert.NotNull(getByPost);
            Assert.Equal(post.PostId, getByPost.PostId);
        }

        [Fact(Skip = "PostRepository_GetCommentsByPostId_Success")]
        public async Task PostRepository_GetCommentsByPostId_Success()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();
            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithPostId(Guid.NewGuid())
                .WithName(Lorem.Words(10))
                .WithText(Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

            var post = await _postRepository.AddAsync(newPost);

            for (var i = 0; i < 20; i++)
            {
                var newComment = new Comment
                {
                    PostId = post.PostId,
                    CommentName = Lorem.Words(10),
                    Email = Lorem.Email()
                };

                await _commentRepository.AddAsync(newComment);
            }

            //Act
            var getByPost = await _postRepository.GetByIdWithCommentsAsync(post.PostId);

            //Assert
            Assert.NotNull(getByPost);
            Assert.Equal(post.PostId, getByPost.PostId);
            Assert.True(post.Comment.Count == 10);
        }

        [Fact(Skip = "PostRepository_GetCommentsByPostIdWWithPaging_ReturnsPaginatedResults")]
        public async Task PostRepository_GetCommentsByPostIdWWithPaging_ReturnsPaginatedResults()
        {
            //Arrange
            var newBlog = BlogBuilder.Default();
            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithPostId(Guid.NewGuid())
                .WithName(Lorem.Words(10))
                .WithText(Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

            var post = await _postRepository.AddAsync(newPost);

            for (var i = 0; i < 10; i++)
            {
                var newComment = new Comment
                {
                    PostId = post.PostId,
                    CommentName = Lorem.Words(10),
                    Email = Lorem.Email()
                };

                await _commentRepository.AddAsync(newComment);
            }

            //Act
            var getByPost = await _postRepository.GetByIdWithCommentsAsync(post.PostId, 2,6);

            //Assert
            Assert.NotNull(getByPost);
            Assert.Equal(post.PostId, getByPost.PostId);
            Assert.True(post.Comment.Count == 6);
        }
    }
}