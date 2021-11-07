using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogWebApi.Domain;
using BlogWebApi.WebApi;
using FluentAssertions;
using LoremNET;
using Newtonsoft.Json;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Posts
{
    [Collection("DatabaseCollectionFixture")]
    public class GetByPostByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GetByPostByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "GetBy_PostId_Ok")]
        public async Task GetBy_PostId_Ok()
        {
            // Arrange
            var blogResponseMessage = await _client.PostAsync("/api/blogs/",
                new StringContent(JsonConvert.SerializeObject(new Blog { BlogName = "GetBy_PostId_Ok" }),
                    Encoding.UTF8,
                    "application/json"));
            var blogContent = await blogResponseMessage.Content.ReadAsStringAsync();

            var blog = JsonConvert.DeserializeObject<Blog>(blogContent);

            var newPost = new Post
            {
                BlogId = blog.BlogId,
                PostName = "GetBy_PostId",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));

            var post = JsonConvert.DeserializeObject<Post>(await postResponseMessage.Content.ReadAsStringAsync());

            // Act
            var responseMessage = await _client.GetAsync($"/api/posts/{post.PostId}");
            var content = await responseMessage.Content.ReadAsStringAsync();

            var postById = JsonConvert.DeserializeObject<Post>(content);

            // Assert
            postById.Should().BeOfType<Post>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
