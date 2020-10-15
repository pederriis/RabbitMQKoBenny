using System;
using System.Collections.Generic;
using System.Text;
using CowService.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CowService
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
            // NuGet: Microsoft.Extensions.Http
            //services.AddHttpClient("LoginService", c => { c.BaseAddress = new Uri("http://localhost:5000"); });

            services.AddHttpClient<LoginClient>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseExceptionHandler("/Public/Error");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Public}/{action=Index}/{id?}");
            });
        }
    }
}