using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProxyKit;

namespace DirectProxy
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
            services.AddProxy();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var server = Configuration["Settings:TargetServer"]; 
            app.RunProxy(context => context
            .ForwardTo(server)
            .AddXForwardedHeaders()
            .Send());
        }
    }
}
