using LSCore.Contracts.Entities;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;

namespace LSCore.Repository;

public class LSCoreRepositoryBase<TEntity>(ILSCoreDbContext dbContext)
    where TEntity : LSCoreEntity
{
    public virtual TEntity Get(long id) =>
        GetOrDefault(id) ?? throw new LSCoreNotFoundException();
    
    public virtual TEntity? GetOrDefault(long id) =>
        dbContext.Set<TEntity>().FirstOrDefault(x => x.IsActive && x.Id == id);
    
    public virtual IQueryable<TEntity> GetMultiple() =>
        dbContext.Set<TEntity>().Where(x => x.IsActive);
}