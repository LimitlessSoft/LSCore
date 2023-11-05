namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreMap<TEntity, TRequest> where TEntity : ILSCoreEntityBase
    {
        void Map(TEntity entity, TRequest request);
    }
}
