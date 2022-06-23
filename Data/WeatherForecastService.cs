using CompleteQuartzExample.Data.IJobs;
using CompleteQuartzExample.Data.QuartzService;
using CompleteQuartzExample.Hubs;

using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CompleteQuartzExample.Data
{

    public class WeatherForecastService: IWeather
    {
      
        internal readonly IScheduler scheduler;
      
        public WeatherForecastService(IScheduler quartz)
        {
            scheduler = quartz;
        
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      //  private UpdateHub hub = new UpdateHub();
        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }

   
        
    }
}
