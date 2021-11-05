using BlogWebApi.Domain;

namespace BlogWebApi.Application.Interfaces
{
    public interface ICommentRepository : IAsyncRepository<Comment>
    {
    }
}