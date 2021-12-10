using BlogWebApi.Application.Interfaces;
using BlogWebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebApi.Infrastructure
{
    public static class ServiceCollectionConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<BlogDbContext>( options =>
                {
                    options.UseInMemoryDatabase(databaseName: "inmemblogdb");
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("BlogDbConnection");

                services.AddDbContext<BlogDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }

            return services;
        }
    }
}