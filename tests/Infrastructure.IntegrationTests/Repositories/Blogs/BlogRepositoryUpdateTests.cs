﻿using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryUpdateTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public BlogRepositoryUpdateTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(_fixture.BlogDbContext);
        }

        [Fact(DisplayName = "BlogRepository_UpdateAsync_Success")]
        public async Task BlogRepository_UpdateAsync_Success()
        {
            //Arrange
            var newBlog = new Blog
            {
                BlogName = Lorem.Words(10, true),
                BlogId = Guid.NewGuid()
            };

            var blog = await _blogRepository.AddAsync(newBlog);

            //Act
            blog.BlogName = "Updated Blog";
            await _blogRepository.UpdateAsync(blog);

            //Assert
            var updatedBlog = await _blogRepository.GetByIdAsync(blog.BlogId);

            Assert.Equal(newBlog.BlogId, updatedBlog.BlogId);
            Assert.Equal(newBlog.BlogName, updatedBlog.BlogName);
        }
    }
}