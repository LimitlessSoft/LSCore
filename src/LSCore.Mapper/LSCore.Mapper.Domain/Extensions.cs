using LSCore.DependencyInjection;
using LSCore.Mapper.Contracts;

namespace LSCore.Mapper.Domain;

public static class Extensions
{
	public static TDestination ToDto<TSource, TDestination>(this TSource source)
		where TSource : class
		where TDestination : class
	{
		if (Container.ServiceProvider == null)
			throw new Exception(
				"To be able to use ToDto method, you must call IHost.UseLSCoreDependencyInjection() as first method after you build application."
			);

		var dtoMapper = (ILSCoreDtoMapper<TSource, TDestination>?)
			Container.ServiceProvider.GetService(typeof(ILSCoreDtoMapper<TSource, TDestination>));
		if (dtoMapper == null)
			throw new ArgumentNullException(
				$"Dto mapper <{typeof(TSource)}, {typeof(TDestination)}> not found."
			);

		return dtoMapper.ToDto(source);
	}

	public static List<TDestination> ToDtoList<TSource, TDestination>(
		this IEnumerable<TSource> sender
	)
		where TSource : class
		where TDestination : class =>
		sender.Select(entity => entity.ToDto<TSource, TDestination>()).ToList();
}
