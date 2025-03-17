namespace LSCore.Mapper.Contracts;

public interface ILSCoreMapper<in TSource, out TDestination>
	where TSource : class
	where TDestination : class
{
	TDestination ToMapped(TSource source);
}
