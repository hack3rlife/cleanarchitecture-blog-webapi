﻿using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using LoremNET;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class BlogRepositoryListAllTests
    {
        private readonly BlogRepository _blogRepository;
        private readonly DatabaseFixture _fixture;

        public BlogRepositoryListAllTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(_fixture.BlogDbContext);
        }

        [Fact(DisplayName = "BlogRepository_ListAllAsyncWithPagination_RespectsPaginationValues")]
        public async Task BlogRepository_ListAllAsyncWithPagination_RespectsPaginationValues()
        {
            //Arrange
            for (var i = 0; i < 10; i++)
            {
                var blog = new Blog
                {
                    BlogName = Lorem.Words(10, true),
                    BlogId = Guid.NewGuid()
                };
                await _blogRepository.AddAsync(blog);
            }

            var skip = 0;
            var take = 5;

            //Act
            var blogs = await _blogRepository.ListAllAsync(skip, take);

            //Assert
            Assert.NotNull(blogs);
            Assert.True(blogs.Count == take);
        }

        [Fact(DisplayName = "BlogRepository_ListAllAsyncUsingIncorrectPagingValues_ShouldUseDefaultPagingValues")]
        public async Task BlogRepository_ListAllAsyncUsingIncorrectPagingValues_ShouldUseDefaultPagingValues()
        {
            //Arrange
            for (var i = 0; i < 20; i++)
            {
                var blog = new Blog
                {
                    BlogName = Lorem.Words(10, true),
                    BlogId = Guid.NewGuid()
                };
                await _blogRepository.AddAsync(blog);
            }

            var skip = -1;
            var take = -1;

            //Act
            var blogs = await _blogRepository.ListAllAsync(skip, take);

            //Assert
            Assert.NotNull(blogs);
            Assert.True(blogs.Count == 10);
        }
    }
}