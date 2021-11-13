using BlogWebApi.Domain;
using System;
using System.Threading.Tasks;

namespace BlogWebApi.Application.Interfaces
{
    public interface IBlogRepository : IAsyncRepository<Blog>
    {
        /// <summary>
        /// Get a single post and its related posts
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Blog> GetByIdWithPostsAsync(Guid blogId, int skip, int take);
    }
}