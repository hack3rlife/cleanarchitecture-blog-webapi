using BlogWebApi.Application;
using BlogWebApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BlogWebApi.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "BlogWebApi",
                        Description = "ASP .NET WebAPI project sample using [Clean Architecture Template](https://github.com/hack3rlife/cleanarchitecture-webapi-template)",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "hack3rlife",
                            Email = "admin@hack3rlife.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Microsoft Public License (MS-PL)",
                            Url = new Uri("https://opensource.org/licenses/MS-PL")
                        }
                    });
                
                //Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //Registering Application Layer Dependencies
            services.AddApplicationServices(Configuration);

            //Registering Infrastructure Layer Dependencies
            services.AddInfrastructureServices(Configuration);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogWebAPi");
                c.RoutePrefix = string.Empty;
                c.ConfigObject.SupportedSubmitMethods = new List<SubmitMethod>()
                {
                    SubmitMethod.Delete,
                    SubmitMethod.Get,
                    SubmitMethod.Patch,
                    SubmitMethod.Post,
                    SubmitMethod.Put
                };
                c.EnableDeepLinking();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
