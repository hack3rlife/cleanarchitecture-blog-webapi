using System;
using System.Threading.Tasks;

namespace BlogWebApi.Domain.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
        Task<Post> GetByIdWithCommentsAsync(Guid postId, int skip, int take);
    }
}