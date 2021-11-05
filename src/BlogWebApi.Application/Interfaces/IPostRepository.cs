using System;
using System.Threading.Tasks;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<Post> GetByIdWithCommentsAsync(Guid postId, int skip, int take);
    }
}