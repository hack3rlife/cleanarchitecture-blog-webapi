using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
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

        public async Task UpsertStatusAsync()
        {
           await blogDbContext.Status.AddAsync(
                       new Status
                       {
                           Started = DateTime.UtcNow.ToString("s"),
                           Server = Environment.MachineName,
                           OsVersion = Environment.OSVersion.ToString(),
                           AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            ProcessorCount = Environment.ProcessorCount                           
                       });

            await blogDbContext.SaveChangesAsync();

        }
    }
}
