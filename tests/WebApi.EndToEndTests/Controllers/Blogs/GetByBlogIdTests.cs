using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using BlogWebApi.WebApi;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class GetByBlogIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GetByBlogIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "GetBy_BlogId_Success")]
        public async Task GetBy_BlogId_Success()
        {
            // Arrange
            const string blogId = "c949db94-5195-498a-afbe-7a90031b3125";

            // Act
            var responseMessage = await _client.GetAsync($"/api/blogs/{blogId}");
            var content = await responseMessage.Content.ReadAsStringAsync();

            var blog = JsonConvert.DeserializeObject<Blog>(content);

            blog.Should().BeOfType<Blog>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "GetBy_BlogId_NotFound")]
        public async Task GetBy_BlogId_NotFound()
        {
            // Arrange
            var blogId = Guid.NewGuid();

            // Act
            var responseMessage = await _client.GetAsync($"/api/blogs/{blogId}");

            var content = JsonConvert.DeserializeObject<string>(await responseMessage.Content.ReadAsStringAsync());

            content.Should().BeOfType<string>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}