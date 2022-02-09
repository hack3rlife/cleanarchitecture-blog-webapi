using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain.Interfaces;
using BlogWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BlogWebApi.Infrastructure
{
    public static class ServiceCollectionConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStatusRepository, StatusRepository>();

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

        public static void ConfigureInfrastructureServices(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var statusService = services.GetRequiredService<IStatusService>();

                    statusService.SetStatusAsync();

                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<IStatusService>>();
                    logger.LogError(exception, "An error occurred while seeding status.");
                }
            }
        }
    }
}