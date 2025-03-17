using LSCore.DependencyInjection;
using LSCore.Mapper.Contracts;

namespace LSCore.Mapper.Domain;

public static class Extensions
{
	public static TDestination ToMapped<TSource, TDestination>(this TSource source)
		where TSource : class
		where TDestination : class
	{
		if (Container.ServiceProvider == null)
			throw new Exception(
				"To be able to use ToMapped method, you must call IHost.UseLSCoreDependencyInjection() as first method after you build application."
			);

		var dtoMapper = (ILSCoreMapper<TSource, TDestination>?)
			Container.ServiceProvider.GetService(typeof(ILSCoreMapper<TSource, TDestination>));
		if (dtoMapper == null)
			throw new ArgumentNullException(
				$"Mapper <{typeof(TSource)}, {typeof(TDestination)}> not found."
			);

		return dtoMapper.ToMapped(source);
	}

	public static List<TDestination> ToMappedList<TSource, TDestination>(
		this IEnumerable<TSource> sender
	)
		where TSource : class
		where TDestination : class =>
		sender.Select(entity => entity.ToMapped<TSource, TDestination>()).ToList();
}
