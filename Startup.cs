using CompleteQuartzExample.Data;
using CompleteQuartzExample.Data.IJobs;
using CompleteQuartzExample.Data.JobFactory;
using CompleteQuartzExample.Data.JobSchedules;

using CompleteQuartzExample.Data.QuartzService;
using CompleteQuartzExample.Data.Services;
using CompleteQuartzExample.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
      services.AddSingleton<IWeather, WeatherForecastService>();
       services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<PrintJob>();
            
                    services.AddSingleton<PoemService>();
            services.AddSingleton<QuartzMultiJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(PrintJob),
                cronExpression: "0/10 * * * * ?")); // run every 10 seconds
      services.AddHostedService<QuartzHostedService>();
       services.AddSingleton(provider => GetScheduler());
            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapHub<UpdateHub>("/updateHub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        private IScheduler GetScheduler()
        {
           
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;
            scheduler.Start();
            return scheduler;

        }




    }
}
