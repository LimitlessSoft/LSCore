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

    public void Insert(TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
        dbContext.SaveChanges();
    }

    public void Insert(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().AddRange(entities);
        dbContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        dbContext.SaveChanges();
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().UpdateRange(entities);
        dbContext.SaveChanges();
    }

    public void SoftDelete(long id)
    {
        var entity = Get(id);
        entity.IsActive = false;
        dbContext.SaveChanges();
    }

    public void HardDelete(long id)
    {
        var entity = Get(id);
        dbContext.Set<TEntity>().Remove(entity);
        dbContext.SaveChanges();
    }

    public void SoftDelete(TEntity entity)
    {
        entity.IsActive = false;
        dbContext.SaveChanges();
    }

    public void HardDelete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
        dbContext.SaveChanges();
    }

    public void SoftDelete(IEnumerable<long> ids)
    {
        var entities = dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id));
        foreach (var entity in entities)
            entity.IsActive = false;
        dbContext.SaveChanges();
    }

    public void HardDelete(IEnumerable<long> ids)
    {
        var entities = dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id));
        dbContext.Set<TEntity>().RemoveRange(entities);
        dbContext.SaveChanges();
    }

    public void SoftDelete(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.IsActive = false;
        dbContext.SaveChanges();
    }

    public void HardDelete(IEnumerable<TEntity> entities)
    {
        dbContext.Set<TEntity>().RemoveRange(entities);
        dbContext.SaveChanges();
    }
}