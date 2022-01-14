using Microsoft.AspNetCore.Mvc;
using Sukt.Identity.Application.Users;

namespace Sukt.Identity.Api.Controllers
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

        private readonly IIdentityUserAppService _identityUserAppService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IIdentityUserAppService identityUserAppService)
        {
            _logger = logger;
            _identityUserAppService=identityUserAppService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            await _identityUserAppService.CreateAsync();


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}