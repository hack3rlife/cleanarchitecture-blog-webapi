using BlogWebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogWebApi.Infrastructure
{
    public class BlogDbContext : DbContext
    {
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        private readonly ILogger<BlogDbContext> _logger;

        public BlogDbContext(DbContextOptions<BlogDbContext> options, ILogger<BlogDbContext> logger)
            : base(options)
        {
            SavedChanges += SavedChangesHandler;
            SaveChangesFailed += SaveChangesFailedHandler;

            _logger = logger;
        }

        private void SaveChangesFailedHandler(object? sender, SaveChangesFailedEventArgs e)
        {
            _logger.LogError(e.Exception, e.ToString());

            var blogDbContext = (BlogDbContext) sender;
            _logger.LogDebug(blogDbContext?.ChangeTracker.DebugView?.LongView);
        }

        public void SavedChangesHandler(object sender, SavedChangesEventArgs e)
        {
            _logger.LogDebug("Changes made {0}", e.EntitiesSavedCount);

            var blogDbContext = (BlogDbContext) sender;
            _logger.LogDebug("Context Id: {0}", blogDbContext.ContextId);
            _logger.LogDebug("Provider Name: {0}", new object[] {blogDbContext.Database.ProviderName});
            _logger.LogDebug(blogDbContext.ChangeTracker.DebugView?.LongView);
        }
    }
}