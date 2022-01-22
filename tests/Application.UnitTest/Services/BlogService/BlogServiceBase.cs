using Application.UnitTest.Mocks;
using AutoMapper;
using BlogWebApi.Application.Profiles;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceBase
    {
        public readonly MockBlogRepository MockBlogRepository;
        public readonly BlogWebApi.Application.Services.BlogService BlogService;

        public BlogServiceBase()
        {
            var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMapper>(); });

            var mapper = configurationProvider.CreateMapper();

            MockBlogRepository = new MockBlogRepository();

            BlogService = new BlogWebApi.Application.Services.BlogService(MockBlogRepository.Object, mapper);

        }
    }
}
