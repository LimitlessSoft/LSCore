namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreDtoMapper<TDto, TEntity>
    {
        public TDto ToDto(TEntity sender);
    }
}
