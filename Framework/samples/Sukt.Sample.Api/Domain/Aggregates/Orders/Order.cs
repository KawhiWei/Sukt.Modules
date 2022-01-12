namespace Sukt.Sample.Api.Domain.Aggregates.Orders
{
    public class Order: AggregateRoot
    {
        public string BuyerId { get; private set; } = default!;

        public Address Address { get; private set; } = default!;

        protected Order() { }

        public Order(string buyerId, Address address)
        {
            BuyerId = buyerId;
            Address = address;
        }
    }
}
