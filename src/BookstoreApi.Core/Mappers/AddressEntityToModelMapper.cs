namespace BookstoreApi.Core.Mappers
{
    public class AddressEntityToModelMapper : IMapper<Entities.Address, Models.Address>
    {
        public Models.Address Map(Entities.Address source)
        {
            return new Models.Address
            {
                Id = source.AddressId,
                Name = source.Name,
                Line1 = source.Line1,
                Line2 = source.Line2,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt
            };
        }
    }
}
