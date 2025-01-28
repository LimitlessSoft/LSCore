using LSCore.Contracts.Entities;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces.Repositories;

namespace LSCore.Repository;

public class LSCoreRepositoryBase<TEntity>(ILSCoreDbContext dbContext) : ILSCoreRepositoryBase<TEntity>
    where TEntity : LSCoreEntity
{
    public virtual TEntity Get(long id) =>
        GetOrDefault(id) ?? throw new LSCoreNotFoundException();
    
    public virtual TEntity? GetOrDefault(long id) =>
        dbContext.Set<TEntity>().FirstOrDefault(x => x.IsActive && x.Id == id);
    
    public virtual IQueryable<TEntity> GetMultiple() =>
        dbContext.Set<TEntity>().Where(x => x.IsActive);

    public void Insert(TEntity entity) =>
        Insert([entity]);

    public void Insert(IEnumerable<TEntity> entities)
    {
        var lsCoreEntities = entities.ToList();
        
        foreach (var entity in lsCoreEntities)
            entity.CreatedAt = DateTime.UtcNow;
        
        dbContext.Set<TEntity>().AddRange(lsCoreEntities);
        dbContext.SaveChanges();
    }

    public void Update(TEntity entity) =>
        Update([entity]);

    public void Update(IEnumerable<TEntity> entities)
    {
        var lsCoreEntities = entities.ToList();

        foreach (var entity in lsCoreEntities)
            entity.UpdatedAt = DateTime.UtcNow;
        
        dbContext.Set<TEntity>().UpdateRange(lsCoreEntities);
        dbContext.SaveChanges();
    }

    public void SoftDelete(long id) =>
        SoftDelete(Get(id));

    public void HardDelete(long id) =>
        HardDelete(Get(id));
    
    public void SoftDelete(TEntity entity) =>
        SoftDelete([entity]);

    public void HardDelete(TEntity entity) =>
        HardDelete([entity]);

    public void SoftDelete(IEnumerable<long> ids) =>
        SoftDelete(dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)));

    public void HardDelete(IEnumerable<long> ids) =>
        HardDelete(dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)));

    public void SoftDelete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActive = false;
        }

        dbContext.SaveChanges();
    }

    public void HardDelete(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().RemoveRange(entities);
        dbContext.SaveChanges();
    }
}