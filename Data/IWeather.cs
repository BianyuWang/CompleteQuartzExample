using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompleteQuartzExample.Data
{
    interface IWeather
    {
  
        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}
