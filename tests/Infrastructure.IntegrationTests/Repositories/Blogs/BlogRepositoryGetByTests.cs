﻿using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryGetByTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly PostRepository _postRepository;

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

            var blog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = guid
            };

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

        [Fact(DisplayName = "BlogRepository_GetPostsByBlogId_Success")]
        public async Task BlogRepository_GetPostsByBlogId_Success()
        {
            //Arrange
            Blog expectedBlog = new Blog();

            for (int i = 0; i < 10; i++)
            {
                var newBlog = new Blog
                {
                    BlogName = Lorem.Words(10, true),
                    BlogId = Guid.NewGuid()
                };

                expectedBlog = await _blogRepository.AddAsync(newBlog);

                for (var j = 0; j < 20; j++)
                {

                    var newPost = new Post
                    {
                        PostId = Guid.NewGuid(),
                        PostName = Lorem.Words(10),
                        Text = Lorem.Sentence(100),
                        BlogId = expectedBlog.BlogId,
                    };
                    _ = await _postRepository.AddAsync(newPost);
                }
            }

            //Act
            var blog = await _blogRepository.GetByIdWithPostsAsync(expectedBlog.BlogId);

            //Assert
            Assert.NotNull(blog);
            Assert.Equal(expectedBlog.BlogId, blog.BlogId);
            Assert.True(blog.Post.Count == 10);
        }

        [Fact(DisplayName = "BlogRepository_GetPostsByBlogIdWithPaging_ReturnsPaginatedResults")]
        public async Task BlogRepository_GetPostsByBlogIdWithPaging_ReturnsPaginatedResults()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };
            var expectedBlog = await _blogRepository.AddAsync(newBlog);

            for (var i = 0; i < 10; i++)
            {

                var newPost = new Post
                {
                    PostId = Guid.NewGuid(),
                    PostName = Lorem.Words(10),
                    Text = Lorem.Sentence(100),
                    BlogId =expectedBlog.BlogId,
                };
                _ = await _postRepository.AddAsync(newPost);
            }

            //Act
            var blog = await _blogRepository.GetByIdWithPostsAsync(expectedBlog.BlogId, 0, 5);

            //Assert
            Assert.NotNull(blog);
            Assert.Equal(expectedBlog.BlogId, blog.BlogId);
            Assert.True(blog.Post.Count == 5);
        }
    }
}