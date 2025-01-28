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

    public virtual void Insert(TEntity entity) =>
        Insert([entity]);

    public virtual void Insert(IEnumerable<TEntity> entities)
    {
        var lsCoreEntities = entities.ToList();
        
        foreach (var entity in lsCoreEntities)
            entity.CreatedAt = DateTime.UtcNow;
        
        dbContext.Set<TEntity>().AddRange(lsCoreEntities);
        dbContext.SaveChanges();
    }

    public virtual void Update(TEntity entity) =>
        Update([entity]);

    public virtual void Update(IEnumerable<TEntity> entities)
    {
        var lsCoreEntities = entities.ToList();

        foreach (var entity in lsCoreEntities)
            entity.UpdatedAt = DateTime.UtcNow;
        
        dbContext.Set<TEntity>().UpdateRange(lsCoreEntities);
        dbContext.SaveChanges();
    }

    public virtual void SoftDelete(long id) =>
        SoftDelete(Get(id));

    public virtual void HardDelete(long id) =>
        HardDelete(Get(id));
    
    public virtual void SoftDelete(TEntity entity) =>
        SoftDelete([entity]);

    public virtual void HardDelete(TEntity entity) =>
        HardDelete([entity]);

    public virtual void SoftDelete(IEnumerable<long> ids) =>
        SoftDelete(dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)));

    public virtual void HardDelete(IEnumerable<long> ids) =>
        HardDelete(dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)));

    public virtual void SoftDelete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActive = false;
        }

        dbContext.SaveChanges();
    }

    public virtual void HardDelete(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().RemoveRange(entities);
        dbContext.SaveChanges();
    }
}