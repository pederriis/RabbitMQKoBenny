using System;
using System.Collections.Generic;
using System.Text;
using cowsumer3.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace cowsumer3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Storage.IStorage, Storage.InMemoryStorage>();
           // services.AddHttpClient<InterService.MasterData>(p => p.BaseAddress = new Uri("http://localhost:10003/"));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "cowsumer", Version = "v1" });
            });

           
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            app.UseStaticFiles();  // NuGet: Microsoft.AspNetCore.StaticFiles
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
