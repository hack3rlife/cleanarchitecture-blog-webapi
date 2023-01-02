using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain.Interfaces;
using BlogWebApi.Domain;
using Moq;

namespace Application.UnitTest.Mocks
{
    public class MockBlogRepository : Mock<IBlogRepository>
    {
        /// <summary>
        /// Setup the mock to return a List
        /// </summary>
        /// <param name="returnBlogs">Specifies the value to return after the setup </param>
        /// <returns><see cref="List{Blog}"/></returns>
        public async Task<MockBlogRepository> MockSetupListAllAsync(List<Blog> returnBlogs)
        {
            Setup(x => x.ListAllAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.Run(() => returnBlogs))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyListAllAsync(int skip, int take, Times times)
        {
            Verify(mock => mock.ListAllAsync(skip, take),  times);
            return this;
        }

        /// <summary>
        /// Setup the mock to return a single <see cref="Blog"/> without comments
        /// </summary>
        /// <param name="blog">The <see cref="Post"/> to be returned by the mock</param>
        /// <returns>A <see cref="Post"/> object</returns>
        public async Task<MockBlogRepository> MockSetupGetByIdAsync(Blog blog)
        {
            Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(await Task.Run(() => blog))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// Verify that the mock executes <see cref="Blog.GetByIdAsync(System.Guid)"/> <paramref name="times"/>
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyGetByIdAsync(Blog blog, Times times)
        {
            Verify(mock => mock.GetByIdAsync(blog.BlogId), times);
            return this;
        }

        public async Task<MockBlogRepository> MockSetupGetByIdWithPostsAsync(Guid blogId, Blog returnsBlog)
        {
            Setup(x => x.GetByIdWithPostsAsync(It.Is<Guid>(b => b == blogId), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.Run(() => returnsBlog))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="take"></param>
        /// <param name="returnsBlog"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<MockBlogRepository> MockSetupGetByIdWithPostsAsync(Guid blogId, int skip, int take, Blog returnsBlog)
        {
            Setup(x => x.GetByIdWithPostsAsync(It.Is<Guid>(b => b == blogId), skip, take))
                .ReturnsAsync(await Task.Run(() => returnsBlog))
                .Verifiable();
            return this;
        }

        public MockBlogRepository MockVerifyGetByIdWithPostsAsync(Guid blogId, Times times)
        {
            Verify(mock => mock.GetByIdWithPostsAsync(blogId, 0, 10), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="take"></param>
        /// <param name="times"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyGetByIdWithPostsAsync(Guid blogId, int skip, int take, Times times)
        {
            Verify(mock => mock.GetByIdWithPostsAsync(blogId, skip, take), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockBlogRepository MockSetupAdd()
        {
            Setup(x => x.AddAsync(It.IsAny<Blog>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnsBlog"></param>
        /// <returns></returns>
        public async Task<MockBlogRepository> MockSetupAddAsync(Blog returnsBlog)
        {
            Setup(x => x.AddAsync(It.IsAny<Blog>()))
                .ReturnsAsync(await Task.Run(() => returnsBlog))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newBlog"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyAddAsync(Times times)
        {
            Verify(mock => mock.AddAsync(It.IsAny<Blog>()), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockBlogRepository MockSetupUpdateAsync()
        {
            Setup(x => x.UpdateAsync(It.IsAny<Blog>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyUpdateAsync(Times times)
        {
            Verify(mock => mock.UpdateAsync(It.IsAny<Blog>()), times);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MockBlogRepository MockSetupDeleteAsync()
        {
            Setup(x => x.DeleteAsync(It.IsAny<Blog>()))
                .Verifiable();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <returns></returns>
        public MockBlogRepository MockVerifyDeleteAsync(Times times)
        {
            Verify(mock => mock.DeleteAsync(It.IsAny<Blog>()), times);
            return this;
        }
    }
}
