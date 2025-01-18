using LSCore.Contracts.Interfaces;
using LSCore.DependencyInjection;

namespace LSCore.Domain.Extensions;

public static class EntityExtensions
{
    public static TDestination ToDto<TSource, TDestination>(this TSource entity)
        where TSource : class
    {
        if (Container.ServiceProvider == null)
            throw new Exception("To be able to use ToDto method, you must call IHost.UseLSCoreDependencyInjection() as first method after you build application.");
        
        var dtoMapper = (ILSCoreDtoMapper<TSource, TDestination>?)Container.ServiceProvider.GetService(typeof(ILSCoreDtoMapper<TSource, TDestination>));
        if(dtoMapper == null)
            throw new ArgumentNullException($"Dto mapper <{typeof(TSource)}, {typeof(TDestination)}> not found.");
        
        return dtoMapper.ToDto(entity);
    }

    public static List<TDestination> ToDtoList<TSource, TDestination>(this IEnumerable<TSource> sender)
        where TSource : class =>
            sender.Select(entity => entity.ToDto<TSource, TDestination>()).ToList();
}
