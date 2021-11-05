using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Interfaces
{
    public interface IBlogService
    {
        /// <summary>
        /// Get all blogs
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<Blog>> GetAll(int skip, int take);

        /// <summary>
        /// Get a single blog with no posts details
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        Task<Blog> GetBy(Guid blogId);

        /// <summary>
        /// Get a single blogs with post details
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<Blog> GetPostsBy(Guid blogId, int skip, int take);

        /// <summary>
        /// Add a blog to the data source
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task<Blog> Add(Blog blog);

        /// <summary>
        /// Update a existing blog in the data source
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task Update(Blog blog);

        /// <summary>
        /// Delete an existing blog from the data source
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        Task Delete(Guid blogId);
    }
}
