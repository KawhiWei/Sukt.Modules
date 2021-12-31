using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.AspNetCore.ApiResults;
using Sukt.Module.Core.DomainResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.AuthServer.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiResultWrap]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task Get()
        {
            await Task.CompletedTask;


            //return new DomainResult("asdasdasdsa");
        }
    }
}