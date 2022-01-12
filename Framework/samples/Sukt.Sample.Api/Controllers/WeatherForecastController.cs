using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.Repositories;
using Sukt.Sample.Api.Domain.Aggregates.Orders;

namespace Sukt.Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAggregateRootRepository<Order, string> _aggregateRootRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAggregateRootRepository<Order, string> aggregateRootRepository)
        {
            _logger = logger;
            _aggregateRootRepository = aggregateRootRepository;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            return await _aggregateRootRepository.NoTrackEntities.ToListAsync();
        }
        [HttpPost]
        public  async Task CreateOrderAsync()
        {
            await _aggregateRootRepository.InsertAsync(new Order("1212", new Address("asdasda", "adasda", "adasdaad")));
        }
        [HttpPost("{id}")]
        public async Task DeleteOrderAsync(string id)
        {
            await _aggregateRootRepository.DeleteAsync(id);
        }
    }
}