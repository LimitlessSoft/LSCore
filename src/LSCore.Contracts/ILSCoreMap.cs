namespace LSCore.Contracts
{
    public interface ILSCoreMap<TEntity, TRequest> where TEntity : ILSCoreEntity
    {
        void Map(TEntity entity, TRequest request);
    }
}
