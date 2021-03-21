using System;
using System.Collections.Generic;

namespace BookstoreApi.Core.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public IList<Address> Addresses { get; } = new List<Address>();
    }
}
