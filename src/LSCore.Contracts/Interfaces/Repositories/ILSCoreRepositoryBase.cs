using LSCore.Contracts.Entities;

namespace LSCore.Contracts.Interfaces.Repositories;

public interface ILSCoreRepositoryBase<TEntity> where TEntity : LSCoreEntity
{
    TEntity Get(long id);
    TEntity? GetOrDefault(long id);
    IQueryable<TEntity> GetMultiple();
    void Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    void UpdateOrInsert(TEntity entity);
    void SoftDelete(long id);
    void HardDelete(long id);
    void SoftDelete(TEntity entity);
    void HardDelete(TEntity entity);
    void SoftDelete(IEnumerable<long> ids);
    void HardDelete(IEnumerable<long> ids);
    void SoftDelete(IEnumerable<TEntity> entities);
    void HardDelete(IEnumerable<TEntity> entities);
}