using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlogWebApi.Application.Dto;
using BlogWebApi.Domain;
using BlogWebApi.WebApi;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class GetBlogsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GetBlogsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_Blogs_Ok()
        {
            // Arrange

            // Act
            var responseMessage = await _client.GetAsync("/api/blogs/");
            var content = await responseMessage.Content.ReadAsStringAsync();

            var blogs = JsonConvert.DeserializeObject<List<BlogDetailsResponseDto>>(content);

            // Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
