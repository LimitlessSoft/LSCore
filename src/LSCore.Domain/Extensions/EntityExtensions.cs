using LSCore.Contracts.Interfaces;

namespace LSCore.Domain.Extensions
{
    public static class EntityExtensions
    {
        public static TDto ToDto<TDto, TEntity>(this TEntity entity)
            where TEntity : class
        {
            var dtoMapper = LSCoreDomainConstants.Container?.TryGetInstance<ILSCoreDtoMapper<TDto, TEntity>>();
            if(dtoMapper == null)
                throw new ArgumentNullException(nameof(dtoMapper));

            return dtoMapper.ToDto(entity);
        }

        public static List<TDto> ToDtoList<TDto, TEntity>(this IEnumerable<TEntity> sender)
            where TEntity : class
        {
            var dtoList = new List<TDto>();

            // ToDo: Here we call ToDto which runs `TryGetInstance` on container each time.
            // Implement that we can pass instance of dto mapper so we skip getting instance each time
            foreach (var entity in sender)
                dtoList.Add(entity.ToDto<TDto, TEntity>());

            return dtoList;
        }
    }
}
