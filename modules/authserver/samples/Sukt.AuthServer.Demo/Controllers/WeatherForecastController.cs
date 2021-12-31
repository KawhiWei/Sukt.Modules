using Microsoft.AspNetCore.Mvc;
using Sukt.AuthServer.Demo;
using Sukt.AuthServer.Domain.Aggregates.Applications;
using Sukt.Module.Core.Repositories;

namespace Sukt.AuthServer.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISuktApplicationDomainService _suktApplicationDomainService;
        private readonly IAggregateRootRepository<SuktApplication, string> _repository;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISuktApplicationDomainService suktApplicationDomainService, IAggregateRootRepository<SuktApplication, string> repository)
        {
            _logger = logger;
            _suktApplicationDomainService = suktApplicationDomainService;
            _repository=repository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var suktApplication= await _suktApplicationDomainService.CreateAsync("asdasdas","ASDASDA");
            await _repository.InsertAsync(suktApplication);
            var entity = await _repository.GetByIdAsync(suktApplication.Id);
            entity.SetClientName("ÎÒÊÇÄãµù");
            var count =await _repository.UpdateAsync(entity);
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