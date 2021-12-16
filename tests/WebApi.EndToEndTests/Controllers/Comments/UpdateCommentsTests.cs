using BlogWebApi.Application.Dto;
using BlogWebApi.WebApi;
using FluentAssertions;
using LoremNET;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebApi.EndToEndTests.Controllers.Comments
{
    [Collection("DatabaseCollectionFixture")]
    public class UpdateCommentsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public UpdateCommentsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Update_Comment_Ok")]
        public async Task Update_Comment_Ok()
        {
            // Arrange
            var blogResponseMessage = await _client.PostAsync("/api/blogs/",
                new StringContent(JsonConvert.SerializeObject(new BlogAddRequestDto { BlogName = "Update_Comment_Ok" }),
                    Encoding.UTF8,
                    "application/json"));

            var blog = JsonConvert.DeserializeObject<BlogByIdResponseDto>(await blogResponseMessage.Content.ReadAsStringAsync());

            var newPost = new PostAddRequestDto
            {
                BlogId = blog.BlogId,
                PostName = "Update_Comment_Post",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));
            var post = JsonConvert.DeserializeObject<PostResponseDto>(await postResponseMessage.Content.ReadAsStringAsync());

            var newComment = new CommentAddRequestDto
            {
                CommentName = "Update_Comment_Comment",
                Email = Lorem.Email(),
                PostId = post.PostId
            };

            var commentResponseMessage = await _client.PostAsync("/api/comments/",
                new StringContent(JsonConvert.SerializeObject(newComment),
                    Encoding.UTF8,
                    "application/json"));

            var comment = JsonConvert.DeserializeObject<CommentResponseDto>(await commentResponseMessage.Content.ReadAsStringAsync());

            var commentDetailsResponseMessage = await _client.GetAsync($"/api/comments/{comment?.CommentId}");

            var updateComment = JsonConvert.DeserializeObject<CommentDetailsResponseDto>(await commentDetailsResponseMessage.Content.ReadAsStringAsync());
            updateComment.CommentName = Lorem.Sentence(2);

            // Act
            var responseMessage = await _client.PutAsync("/api/comments/",
                new StringContent(JsonConvert.SerializeObject(updateComment),
                    Encoding.UTF8,
                    "application/json"));

            var result = JsonConvert.DeserializeObject<CommentResponseDto>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            result.Should().BeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
