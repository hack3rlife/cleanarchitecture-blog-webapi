using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebApi.Infrastructure
{
    public static class ServiceCollectionConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                Console.WriteLine("# # # # # ConnectionString: UseInMemoryDatabase # # # # #");
                services.AddDbContext<BlogDbContext>(options => options.UseInMemoryDatabase(databaseName: "inmemblogdb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("BlogDbConnection");

                Console.WriteLine("# # # # # ConnectionString: {connectionString} # # # # #");
                services.AddDbContext<BlogDbContext>(c => c.UseSqlServer(connectionString));
            }

            return services;
        }
    }
}