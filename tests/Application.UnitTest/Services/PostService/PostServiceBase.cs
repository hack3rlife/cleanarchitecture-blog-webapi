using Application.UnitTest.Mocks;
using BlogWebApi.Application.Interfaces;

namespace Application.UnitTest.Services.PostService
{
    public class PostServiceBase
    {
        public readonly MockPostsRepository MockPostRepository;
        public readonly IPostService PostService;

        public const int Skip = 0; //default pagination value
        public const int Take = 10; //default pagination value

        public PostServiceBase()
        {
            MockPostRepository = new MockPostsRepository();
            PostService = new BlogWebApi.Application.Services.PostService(MockPostRepository.Object);
        }
    }
}