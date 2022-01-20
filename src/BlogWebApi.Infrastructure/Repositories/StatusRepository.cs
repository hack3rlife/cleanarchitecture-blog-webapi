using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly BlogDbContext blogDbContext;

        public StatusRepository(BlogDbContext cleanArchitectureDbContext)
        {
            blogDbContext = cleanArchitectureDbContext;
        }

        public async Task<Status> GetStatusAsync()
        {
            return await blogDbContext.Status.FirstOrDefaultAsync();
        }
    }
}
