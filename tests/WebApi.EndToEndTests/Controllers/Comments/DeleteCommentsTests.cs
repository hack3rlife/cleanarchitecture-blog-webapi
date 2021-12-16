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
    public class DeleteCommentsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public DeleteCommentsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Delete_Comment_Ok")]
        public async Task Delete_Comment_Ok()
        {
            // Arrange
            var blogResponseMessage = await _client.PostAsync("/api/blogs/",
                new StringContent(JsonConvert.SerializeObject(new BlogAddRequestDto { BlogName = "Delete_Comment_Ok" }),
                    Encoding.UTF8,
                    "application/json"));

            var blog = JsonConvert.DeserializeObject<BlogByIdResponseDto>(await blogResponseMessage.Content.ReadAsStringAsync());

            var newPost = new PostAddRequestDto
            {
                BlogId = blog.BlogId,
                PostName = "Delete_Comment",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));
            var post = JsonConvert.DeserializeObject<PostResponseDto>(await postResponseMessage.Content.ReadAsStringAsync());

            var newComment = new CommentAddRequestDto
            {
                CommentName = "DeleteComment",
                Email = Lorem.Email(),
                PostId = post.PostId
            };

            var commentResponseMessage = await _client.PostAsync("/api/comments/",
                new StringContent(JsonConvert.SerializeObject(newComment),
                    Encoding.UTF8,
                    "application/json"));

            var comment = JsonConvert.DeserializeObject<CommentResponseDto>(await commentResponseMessage.Content.ReadAsStringAsync());

            // Act
            var responseMessage = await _client.DeleteAsync($"/api/comments/{comment.CommentId}");

            var result = JsonConvert.DeserializeObject<CommentResponseDto>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            result.Should().BeNull();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
