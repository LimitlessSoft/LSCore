namespace LSCore.Mapper.Contracts;

public interface ILSCoreDtoMapper<in TSource, out TDestination>
	where TSource : class
	where TDestination : class
{
	TDestination ToDto(TSource source);
}
