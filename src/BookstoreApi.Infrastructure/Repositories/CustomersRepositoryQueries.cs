namespace BookstoreApi.Infrastructure.Repositories
{
    internal static class CustomersRepositoryQueries
    {
        public const string GetAll =
@"SELECT COUNT(*) FROM Customers
SELECT * FROM Customers";

        public const string GetById =
@"SELECT * FROM Customers WHERE CustomerId = @Id";
    }
}
