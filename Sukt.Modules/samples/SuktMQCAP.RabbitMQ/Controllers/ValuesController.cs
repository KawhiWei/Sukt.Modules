using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.MQTransaction;
using System;

namespace SuktMQCAP.RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMQTransactionPublisher _transactionPublisher;

        public ValuesController(IMQTransactionPublisher transactionPublisher)
        {
            _transactionPublisher = transactionPublisher;
        }
        [HttpGet]
        [Route("~/test")]
        public IActionResult Test()
        {
            _transactionPublisher.Publish("sukt.mqtransaction", "mqtransaction.keys", DateTime.Now);
            return Ok();
        }
    }
}
