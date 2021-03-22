namespace BookstoreApi.Core.Mappers
{
    public class CustomerEntityToModelMapper : IMapper<Entities.Customer, Models.Customer>
    {
        public Models.Customer Map(Entities.Customer source)
        {
            return new Models.Customer
            {
                Id = source.CustomerId,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                Phone = source.Phone,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
        }
    }
}
