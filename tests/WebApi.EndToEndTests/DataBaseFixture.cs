using BlogWebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.EndToEndTests
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseFixture : IDisposable
    {
        private bool _isDisposed;

        public BlogDbContext BlogDbContext { get; set; }

        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: "inmemblogdb")
                .Options;

            BlogDbContext = new BlogDbContext(options);

            BlogDbContext.Database.EnsureCreated();

            BlogDbContextDataSeed.SeedSampleData(BlogDbContext);
        }

        public void Dispose()
        {
            BlogDbContext?.Dispose();
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

        ~DataBaseFixture()
        {
            Dispose(false);
        }
    }
}
