using LSCore.Contracts.Interfaces;

namespace LSCore.Domain.Extensions
{
    public static class EntityExtensions
    {
        public static TDestination ToDto<TSource, TDestination>(this TSource entity)
            where TSource : class
        {
            var dtoMapper = LSCoreDomainConstants.Container?.TryGetInstance<ILSCoreDtoMapper<TSource, TDestination>>();
            if(dtoMapper == null)
                throw new ArgumentNullException($"Dto mapper <{typeof(TSource)}, {typeof(TDestination)}> not found.");

            return dtoMapper.ToDto(entity);
        }

        public static List<TDestination> ToDtoList<TSource, TDestination>(this IEnumerable<TSource> sender)
            where TSource : class =>
                sender.Select(entity => entity.ToDto<TSource, TDestination>()).ToList();
    }
}
