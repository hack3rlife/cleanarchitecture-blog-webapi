using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;

namespace Application.UnitTest.Builders
{
    /// <summary>
    /// SUT BlogService Factory Builder
    /// </summary>
    public  class BlogServiceBuilder
    {
        private IBlogRepository _blogRepository;

        public static BlogServiceBuilder Create()
        {
            return new BlogServiceBuilder();
        }

        public BlogServiceBuilder WithRepository(IBlogRepository blogRepository)
        {
            this._blogRepository = blogRepository;
            return this;
        }

        public BlogService Build()
        {
            return new BlogService(this._blogRepository);
        }
    }
}
