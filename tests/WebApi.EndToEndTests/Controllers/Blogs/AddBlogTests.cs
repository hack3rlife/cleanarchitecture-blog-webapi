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
    public class AddBlogTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public AddBlogTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Add_Blog_Ok")]
        public async Task Add_Blog_Ok()
        {
            // Arrange
            var newBlog = new Blog
            {
                BlogName = "AddBlog"
            };

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(newBlog), Encoding.UTF8, "application/json");

            // Act
            var responseMessage = await _client.PostAsync("/api/blogs/", httpContent);
            var content = await responseMessage.Content.ReadAsStringAsync();

            var blog = JsonConvert.DeserializeObject<Blog>(content);

            // Assert
            blog.Should().BeOfType<Blog>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
