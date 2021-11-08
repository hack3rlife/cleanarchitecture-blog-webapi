using System;
using BlogWebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.IntegrationTests
{
    public class DatabaseFixture : IDisposable
    {
        private bool _isDisposed;
        public readonly IConfiguration Configuration;

        public BlogDbContext BlogDbContext { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DatabaseFixture()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = Configuration.GetConnectionString("BlogDbConnection");
            Console.WriteLine($"* * * * * Database : {connectionString} * * * * *");
            
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase("in-memory")
                //.UseSqlServer(connectionString)
                .Options;

            BlogDbContext = new BlogDbContext(options);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;


            if (disposing)
            {
                BlogDbContext.Dispose();
            }

            _isDisposed = true;
        }

        ~DatabaseFixture()
        {
            Dispose(false);
        }
    }
}