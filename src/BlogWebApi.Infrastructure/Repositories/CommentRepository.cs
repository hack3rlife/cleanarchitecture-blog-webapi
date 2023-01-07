using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}
