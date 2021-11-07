using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using BlogWebApi.WebApi;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Blogs
{
    [Collection("DatabaseCollectionFixture")]
    public class UpdateBlogsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UpdateBlogsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Update_Blog_Success")]
        public async Task Update_Blog_Success()
        {
            // Arrange
            var newBlog = new Blog
            {
                BlogName = LoremNET.Lorem.Words(20)
            };

            HttpContent postHttpContent = new StringContent(JsonConvert.SerializeObject(newBlog), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/blogs/", postHttpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var blog = JsonConvert.DeserializeObject<Blog>(responseContent);

            blog.BlogName = "Updated Blog";

            HttpContent putHttpContent = new StringContent(JsonConvert.SerializeObject(blog), Encoding.UTF8, "application/json");

            // Act
            var responseMessage = await _client.PutAsync("/api/blogs/", putHttpContent);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Blog>(content);

            // Assert
            result.Should().BeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

    }
}
