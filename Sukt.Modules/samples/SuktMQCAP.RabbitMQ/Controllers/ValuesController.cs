using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Sukt.MQTransaction;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SuktMQCAP.RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMQTransactionPublisher _transactionPublisher;
        private ILogger<ValuesController> _logger;
        public ValuesController(IMQTransactionPublisher transactionPublisher,ILogger<ValuesController> logger)
        {
            _logger = logger;
            _transactionPublisher = transactionPublisher;
        }
        [HttpGet]
        [Route("~/test")]
        public async Task<IActionResult> Test([FromQuery] int isrent = 1)
        {
            //1=不租用
            try
            {
                await _transactionPublisher.PublishAsync("sukt.mqtransaction", "mqtransaction.keys", DateTime.Now,isrent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"发送消息失败&&&&&&&&&&&&{ex.Message}");
            }
            //Console.WriteLine("发送成功——————————————————————————————————————》");
            return Ok();
        }
    }
}
