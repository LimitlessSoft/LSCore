using LSCore.Contracts.Entities;

namespace LSCore.Contracts.Interfaces.Repositories;

public interface ILSCoreRepositoryBase<out TEntity> where TEntity : LSCoreEntity
{
    TEntity Get(long id);
    TEntity? GetOrDefault(long id);
    IQueryable<TEntity> GetMultiple();
}