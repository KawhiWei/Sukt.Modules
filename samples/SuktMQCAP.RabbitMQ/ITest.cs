using Sukt.MQTransaction;
using System;

namespace SuktMQCAP.RabbitMQ
{
    public interface ITest : ISuktMQTransactionSubscribe
    {
        void Order(DateTime model);
    }
    public class Test : ITest
    {
        [SuktMQSubscribe(exchange: "sukt.mqtransaction", topicOrRoutingKeyName: "mqtransaction.keys")]
        public void Order(DateTime model)
        {
            //throw new Exception("我报错了");
            Console.WriteLine($"消费成功------>{model.ToString("yyyy-MM-dd hh:mm:ss")}");
        }
        
    }
    public class Model
    {
        public string Name { get; set; }
    }
    public interface ITestA : ISuktMQTransactionSubscribe
    {
        void OrderA(DateTime model);
    }

    public class TestA : ITestA
    {
        
        [SuktMQSubscribe(exchange: "sukt.mqtransactionasda", topicOrRoutingKeyName: "mqtransaction.keysasa", queue: "dasdas")]
        public void OrderA(DateTime model)
        {
            //throw new Exception("我报错了");
            Console.WriteLine($"消费成功------>{model.ToString("yyyy-MM-dd hh:mm:ss")}");
        }
    }
}
