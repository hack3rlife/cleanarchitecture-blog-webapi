using System.Collections.Generic;
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

namespace WebApi.EndToEndTests.Controllers.Comments
{
    [Collection("DatabaseCollectionFixture")]
    public class GetCommentsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GetCommentsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Get_Comments_Ok")]
        public async Task Get_Comments_Ok()
        {
            // Arrange
            var blogResponseMessage = await _client.PostAsync("/api/blogs/",
                new StringContent(JsonConvert.SerializeObject(new Blog { BlogName = "Get_Comments_Blog" }),
                    Encoding.UTF8,
                    "application/json"));

            var blog = JsonConvert.DeserializeObject<Blog>(await blogResponseMessage.Content.ReadAsStringAsync());

            var newPost = new Post
            {
                BlogId = blog.BlogId,
                PostName = "Get_Comments_Post",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));
            var post = JsonConvert.DeserializeObject<Post>(await postResponseMessage.Content.ReadAsStringAsync());

            var newComment = new Comment
            {
                CommentName = "Get_Comments_Comment",
                Email = Lorem.Email(),
                PostId = post.PostId
            };

            var commentResponseMessage = await _client.PostAsync("/api/comments/",
                new StringContent(JsonConvert.SerializeObject(newComment),
                    Encoding.UTF8,
                    "application/json"));

            var comment = JsonConvert.DeserializeObject<Comment>(await commentResponseMessage.Content.ReadAsStringAsync());

            // Act
            var responseMessage = await _client.GetAsync("/api/comments/");

            var comments = JsonConvert.DeserializeObject<List<Comment>>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            comments.Should().BeOfType<List<Comment>>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
