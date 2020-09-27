using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using redlocktst.Constants;
using redlocktst.DistributedLockers;

namespace redlocktst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedLocker _distributedLocker;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IDistributedLocker distributedLocker)
        {
            _logger = logger;
            _distributedLocker = distributedLocker;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await using (await _distributedLocker.AcquireLockAsync(LockResources.GetWeather))
            {
                await Task.Delay(5000);
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToArray();
            }
        }
    }
}