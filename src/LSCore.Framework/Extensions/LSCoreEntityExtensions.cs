using LSCore.Contracts.Interfaces;
using LSCore.Domain;

namespace LSCore.Framework.Extensions
{
    public static class LSCoreEntityExtensions
    {
        public static TDto ToDto<TDto, TEntity>(this TEntity sender)
            where TEntity : class
        {
            var dtoMapper = LSCoreDomainConstants.Container?.TryGetInstance<ILSCoreDtoMapper<TDto, TEntity>>();
            if(dtoMapper == null)
                throw new ArgumentNullException(nameof(dtoMapper));

            return dtoMapper.ToDto(sender);
        }
    }
}
