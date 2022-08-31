using Sukt.Module.Core.Domian;

namespace Sukt.Sample.Api.Domain.Aggregates.Orders
{
    public class Order: AggregateRoot, ISoftDelete
    {
        public string BuyerId { get; private set; } = default!;

        public bool Deleted { get; set; }

        public IReadOnlyCollection<Address> Address { get; private set; } = default!;

        protected Order() { }

        public Order(string buyerId, Address[] address)
        {
            BuyerId = buyerId;
            Address = address;
        }
    }
}
