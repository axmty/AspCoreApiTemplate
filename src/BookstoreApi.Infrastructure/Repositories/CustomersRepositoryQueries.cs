namespace BookstoreApi.Infrastructure.Repositories
{
    internal static class CustomersRepositoryQueries
    {
        public const string GetAll =
@"SELECT COUNT(*) FROM Customers
SELECT * FROM Customers";

        public const string GetById =
@"SELECT * FROM Customers WHERE CustomerId = @Id";

        public const string GetAddresses =
@"SELECT COUNT(*) FROM Addresses WHERE CustomerId = @CustomerId
SELECT * FROM Addresses WHERE CustomerId = @CustomerId";
    }
}
