namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreDtoMapper<in TSource, out TDestination>
    {
        public TDestination ToDto(TSource sender);
    }
}
