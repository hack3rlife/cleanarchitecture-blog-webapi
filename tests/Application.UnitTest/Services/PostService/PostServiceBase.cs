using Application.UnitTest.Mocks;
using AutoMapper;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Profiles;

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
            var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMapper>(); });

            var mapper = configurationProvider.CreateMapper();
            MockPostRepository = new MockPostsRepository();

            PostService = new BlogWebApi.Application.Services.PostService(MockPostRepository.Object, mapper);
        }
    }
}