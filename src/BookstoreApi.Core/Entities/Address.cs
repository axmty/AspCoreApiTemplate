using System;

namespace BookstoreApi.Core.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Postcode { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public Customer Customer { get; set; }
    }
}
