using BlogWebApi.Application.Mappers;
using BlogWebApi.Domain;
using LoremNET;
using System;
using Xunit;

namespace Application.UnitTest.Mappers
{
    public class PostDetailsResponseMapperTests
    {
        [Fact]
        public void PostDetailsResponseMapper_MapToPost_Success()
        {
            // Arrange
            var post = new Post
            {
                BlogId = Guid.NewGuid(),
                PostId = Guid.NewGuid(),
                PostName = Lorem.Words(10),
                Text = Lorem.Sentence(10),

                Comment =
                {
                    new Comment
                    {
                        CommentId = Guid.NewGuid(),
                        CommentName = LoremNET.Lorem.Words(10),
                        Email = Lorem.Email(),
                        PostId = Guid.NewGuid(),
                        CreatedBy = "hackerlife",
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = "hackerlife",
                        LastUpdate = DateTime.UtcNow
                    }
                },

                CreatedBy = "hackerlife",
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = "hackerlife",
                LastUpdate = DateTime.UtcNow

            };

                    // Act
            var detailsResponseDto = PostDetailsResponseMapper.Map(post);

            Assert.Equal(post.BlogId, detailsResponseDto.BlogId);
            Assert.Equal(post.PostId, detailsResponseDto.PostId);
            Assert.Equal(post.PostName, detailsResponseDto.PostName);
            Assert.Equal(post.Text, detailsResponseDto.Text);
            Assert.Equal(post.Comment.Count, detailsResponseDto.Comment.Count);
            Assert.Equal(post.CreatedBy, detailsResponseDto.CreatedBy);
            Assert.Equal(post.CreatedDate, detailsResponseDto.CreatedDate);
            Assert.Equal(post.UpdatedBy, detailsResponseDto.UpdatedBy);
            Assert.Equal(post.LastUpdate, detailsResponseDto.LastUpdate);
        }

        [Fact]
        public void PostDetailsResponseMapper_WhenPostIsNull_ReturnsNullPostDetailsResponseDto()
        {
            // Arrange
            Post post = null;

            // Act
            var detailsResponseDto = PostDetailsResponseMapper.Map(post);

            Assert.Null(detailsResponseDto);
        }
    }
}
