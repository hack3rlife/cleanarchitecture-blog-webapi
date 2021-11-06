using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}
