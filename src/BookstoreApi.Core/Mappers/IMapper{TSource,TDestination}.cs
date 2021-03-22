namespace BookstoreApi.Core.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
