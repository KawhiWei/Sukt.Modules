using Sukt.MQTransaction;

namespace SuktMQCAP.RabbitMQ
{
    public interface ITest:ISuktMQTransactionSubscribe
    {
        void Order(Model model);
    }
    public class Test : ITest
    {
        [SuktMQSubscribe(exchange:"test",topicOrRoutingKeyName:"testname",queue:"woshibaba")]
        public void Order(Model model)
        {
            
        }
    }
    public class Model
    {
        public string Name { get; set; }
    }
}
