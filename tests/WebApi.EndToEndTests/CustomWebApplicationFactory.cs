using System.Linq;
using BlogWebApi.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.EndToEndTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    var descriptor = services.FirstOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<BlogDbContext>));
                    services.Remove(descriptor);

                })
                .ConfigureTestServices(testServices =>
                {
                    testServices.AddDbContext<BlogDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("inmemblogdb");
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        options.EnableSensitiveDataLogging();
                        options.EnableDetailedErrors();
                    });

                    //var context = Services.GetRequiredService<BlogDbContext>();

                    //BlogDbContextDataSeed.SeedSampleData(context);

                });
            //.UseStartup<Startup>(); no need to call it because it was already invoked from Program.CreateHostBuilder
        }
    }
}