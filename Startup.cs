using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using aspnetcoreapp.Hubs;
using System.Device.Gpio;

namespace aspnetcoreapp
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
            services.AddRazorPages();
            //signalR
             services.AddSignalR();
            services.AddHostedService<Worker>();

            services.AddSingleton<GpioController>(s =>
            {
                var controller=new GpioController();
                controller.OpenPin(13,PinMode.Output);
                controller.OpenPin(19,PinMode.Output);
                controller.OpenPin(26,PinMode.Output);

                controller.OpenPin(24,PinMode.Input);
                controller.OpenPin(25,PinMode.Input);
                controller.OpenPin(12,PinMode.Input);
                return controller;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                 endpoints.MapHub<GpioHub>("/gpiohub");
            });
        }
    }
}
