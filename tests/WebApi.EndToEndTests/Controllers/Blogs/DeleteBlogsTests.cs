using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using BlogWebApi.WebApi;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Blogs
{

    [Collection("DatabaseCollectionFixture")]
    public class DeleteBlogsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public DeleteBlogsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Delete_Blog_Success")]
        public async Task Delete_Blog_Success()
        {
            // Arrange
            var newBlog = new Blog
            {
                BlogName = LoremNET.Lorem.Words(20)
            };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(newBlog), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/api/blogs/", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<Blog>(responseContent);

            // Act
            var responseMessage = await _client.DeleteAsync($"/api/blogs/{blog.BlogId}");
            var result = JsonConvert.DeserializeObject<BlogDetailsResponseDto>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            result.Should().BeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "Delete_BlogWhichDoesNotExist_ManagesNotFoundException", Skip = "Feature not developed yet")]
        public async Task Delete_BlogWhichDoesNotExist_ManagesNotFoundException()
        {
            // Arrange
            var blogId = Guid.NewGuid();

            // Act
            var responseMessage = await _client.DeleteAsync($"/api/blogs/{blogId}");

            // Assert
            responseMessage.Should().NotBeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);

        }

    }
}
