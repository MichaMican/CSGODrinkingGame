using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSGODrinkingGameServer.Manager;
using CSGODrinkingGameServer.Interfaces;
using CSGODrinkingGameServer.IO;
using CSGODrinkingGameServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSGODrinkingGameServer.Models.Settings;

namespace CSGODrinkingGameServer
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
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            services.AddControllers();
            services.AddSingleton<IStateHandler, StateManager>();
            services.AddSingleton<IDrinkManager, DrinkManager>();
            services.AddSingleton<IArduinoSerial, ArduinoSerial>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
