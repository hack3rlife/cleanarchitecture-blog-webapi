using BlogWebApi.Domain;
using BlogWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlogWebApi.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly BlogDbContext blogDbContext;

        private readonly int major = 1;
        private static readonly int minor = 0;

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
            var startDate = new DateTime(year: 2021, month: 11, day: 4);
            var build = (int)((DateTime.UtcNow - startDate).TotalDays);
            var revision = (int)DateTime.UtcNow.TimeOfDay.TotalMinutes;

           await blogDbContext.Status.AddAsync(
                       new Status
                       {
                           Started = DateTime.UtcNow.ToString("s"),
                           Server = Environment.MachineName,
                           OsVersion = Environment.OSVersion.ToString(),
                           AssemblyVersion = $"{major}.{minor}.{build}.{revision}",
                           ProcessorCount = Environment.ProcessorCount                           
                       });

            await blogDbContext.SaveChangesAsync();

        }
    }
}
