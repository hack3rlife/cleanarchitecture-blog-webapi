using Application.UnitTest.Builders;
using Application.UnitTest.Mocks;

namespace Application.UnitTest.Services.BlogService
{
    public class BlogServiceBase
    {
        public readonly MockBlogRepository MockBlogRepository;
        public readonly BlogWebApi.Application.Services.BlogService BlogService;

        public BlogServiceBase()
        {
            MockBlogRepository = new MockBlogRepository();

            BlogService = BlogServiceBuilder.Create()
                .WithRepository(MockBlogRepository.Object)
                .Build();

        }
    }
}
