using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll(int skip = 0, int take = 10);
        Task<Post> GetBy(Guid postId);
        Task<Post> GetCommentsBy(Guid postId, int skip = 0, int take = 10);
        Task<Post> Add(Post post);
        Task Update(Post post);
        Task Delete(Guid postId);
    }
}