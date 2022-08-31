using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.Repositories;
using Sukt.Module.Core.UnitOfWorks;
using Sukt.Sample.Api.Domain.Aggregates.Orders;

namespace Sukt.Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAggregateRootRepository<Order, string> _aggregateRootRepository;
        private readonly IAggregateRootRepository<Address, string> _aggregateRootRepository2;
        private readonly SampleDbContext _sampleDbContext;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IAggregateRootRepository<Order, string> aggregateRootRepository, 
            IAggregateRootRepository<Address, string> aggregateRootRepository2,
            SampleDbContext sampleDbContext)
        {
            _logger = logger;
            _aggregateRootRepository = aggregateRootRepository;
            _aggregateRootRepository2 = aggregateRootRepository2;
            _sampleDbContext = sampleDbContext;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            var address1 = new Address("asdasda", "adasda", "adasdaad");
            var address2 = new Address("asdasda", "adasda", "adasdaad");
            var order = new Order("1212", new[] { address1, address2 });

            await _aggregateRootRepository.InsertAsync(order);
            
            await _aggregateRootRepository2.DeleteAsync(address1.Id);

            return await _sampleDbContext.Orders.Include(x => x.Address).FirstAsync(x => x.Id == order.Id);
        }
        [HttpPost]
        public  async Task CreateOrderAsync()
        {
            //await _aggregateRootRepository.InsertAsync(new Order("1212", new Address("asdasda", "adasda", "adasdaad")));
        }
        [HttpPost("{id}")]
        public async Task DeleteOrderAsync(string id)
        {
            await _aggregateRootRepository.DeleteAsync(id);
        }
    }
}