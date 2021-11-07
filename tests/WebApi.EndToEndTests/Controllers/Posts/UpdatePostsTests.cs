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
    public class UpdatePostsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UpdatePostsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Update_Post_Ok")]
        public async Task Update_Post_Ok()
        {
            // Arrange
            var blogResponseMessage = await _client.PostAsync("/api/blogs/",
                new StringContent(JsonConvert.SerializeObject(new Blog { BlogName = "Update_Post_Ok" }),
                    Encoding.UTF8,
                    "application/json"));
            var blogContent = await blogResponseMessage.Content.ReadAsStringAsync();

            var blog = JsonConvert.DeserializeObject<Blog>(blogContent);

            var newPost = new Post
            {
                BlogId = blog.BlogId,
                PostName = "Update_Post",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));

            var post = JsonConvert.DeserializeObject<Post>(await postResponseMessage.Content.ReadAsStringAsync());
            post.Text = Lorem.Paragraph(50, 100);
            
            // Act
           var responseMessage = await _client.PutAsync("/api/posts/",
               new StringContent(JsonConvert.SerializeObject(post),
                   Encoding.UTF8,
                   "application/json"));

           var content = await responseMessage.Content.ReadAsStringAsync();
           var result = JsonConvert.DeserializeObject<Post>(content);

            // Assert
            result.Should().BeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
