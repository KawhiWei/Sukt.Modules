using Sukt.Module.Core.Domian;

namespace Sukt.Sample.Api.Domain.Aggregates.Orders
{
    public class Address : AggregateRoot, ISoftDelete
    {
        protected Address()
        {

        }
        public Address(string name, string phone, string fullAddress)
        {
            Name = name;
            Phone = phone;
            FullAddress = fullAddress;
        }

        public string Name { get; init; }
        public string Phone { get; init; }
        public string FullAddress { get; init; }
        public bool Deleted { get; set; }

    }
}
