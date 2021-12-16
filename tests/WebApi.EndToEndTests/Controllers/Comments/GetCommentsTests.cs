using BlogWebApi.Application.Dto;
using BlogWebApi.WebApi;
using FluentAssertions;
using LoremNET;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                new StringContent(JsonConvert.SerializeObject(new BlogAddRequestDto { BlogName = "Get_Comments_Blog" }),
                    Encoding.UTF8,
                    "application/json"));

            var blog = JsonConvert.DeserializeObject<BlogByIdResponseDto>(await blogResponseMessage.Content.ReadAsStringAsync());

            var newPost = new PostAddRequestDto
            {
                BlogId = blog.BlogId,
                PostName = "Get_Comments_Post",
                Text = Lorem.Sentence(100)
            };

            var postResponseMessage = await _client.PostAsync("/api/posts/",
                new StringContent(JsonConvert.SerializeObject(newPost),
                    Encoding.UTF8,
                    "application/json"));
            var post = JsonConvert.DeserializeObject<PostResponseDto>(await postResponseMessage.Content.ReadAsStringAsync());

            for (var i = 0; i < 5; i++)
            {
                var newComment = new CommentAddRequestDto
                {
                    CommentName = Lorem.Words(10),
                    Email = Lorem.Email(),
                    PostId = post.PostId
                };

                var commentResponseMessage = await _client.PostAsync("/api/comments/",
                    new StringContent(JsonConvert.SerializeObject(newComment),
                        Encoding.UTF8,
                        "application/json"));
            }

            // Act
            var responseMessage = await _client.GetAsync("/api/comments/");

            var comments = JsonConvert.DeserializeObject<List<CommentResponseDto>>(await responseMessage.Content.ReadAsStringAsync());

            // Assert
            comments.Should().BeOfType<List<CommentResponseDto>>();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
