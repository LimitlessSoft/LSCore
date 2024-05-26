using LSCore.Contracts.Interfaces;
using LSCore.Domain;

namespace LSCore.Framework.Extensions
{
    public static class LSCoreEntityExtensions
    {
        public static TDestination ToDto<TSource, TDestination>(this TSource sender)
            where TSource : class
        {
            var dtoMapper = LSCoreDomainConstants.Container?.TryGetInstance<ILSCoreDtoMapper<TSource, TDestination>>();
            if(dtoMapper == null)
                throw new ArgumentNullException(nameof(dtoMapper));

            return dtoMapper.ToDto(sender);
        }
    }
}
