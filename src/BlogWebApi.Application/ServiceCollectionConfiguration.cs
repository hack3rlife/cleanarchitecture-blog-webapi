using BlogWebApi.Application.Interfaces;
using BlogWebApi.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogWebApi.Application
{
    public static class ServiceCollectionConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // BlogWebAPI Application Services
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }

        public static void ConfigureApplicationServices(this IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}