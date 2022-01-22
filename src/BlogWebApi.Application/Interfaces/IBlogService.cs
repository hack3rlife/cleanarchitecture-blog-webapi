using BlogWebApi.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<IEnumerable<BlogDetailsResponseDto>> GetAll(int skip, int take);

        /// <summary>
        /// Get a single blog with no posts details
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        Task<BlogResponseDto> GetBy(Guid blogId);

        /// <summary>
        /// Get a single blogs with post details
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<BlogDetailsResponseDto> GetPostsBy(Guid blogId, int skip, int take);

        /// <summary>
        /// Add a blog to the data source
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task<BlogDetailsResponseDto> Add(BlogAddRequestDto blog);

        /// <summary>
        /// Update a existing blog in the data source
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        Task Update(BlogUpdateRequestDto blog);

        /// <summary>
        /// Delete an existing blog from the data source
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns></returns>
        Task Delete(Guid blogId);
    }
}
