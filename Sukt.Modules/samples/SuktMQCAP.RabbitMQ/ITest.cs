using Sukt.MQTransaction;
using System;

namespace SuktMQCAP.RabbitMQ
{
    public interface ITest:ISuktMQTransactionSubscribe
    {
        void Order(DateTime model);
    }
    public class Test : ITest
    {
        [SuktMQSubscribe(exchange: "mqtransaction", topicOrRoutingKeyName: "mqtransaction.keys")]
        public void Order(DateTime model)
        {
            //throw new Exception("我报错了");
            Console.WriteLine(model.ToLongDateString());
        }
    }
    public class Model
    {
        public string Name { get; set; }
    }
}
