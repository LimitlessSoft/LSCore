using LSCore.Contracts.Enums.ValidationCodes;
using Microsoft.Extensions.Logging;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Extensions;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using Omu.ValueInjecter;
using LSCore.Contracts;

// ReSharper disable MemberCanBePrivate.Global

namespace LSCore.Domain.Managers;

public abstract class LSCoreManagerBase<TManager>
{
    private readonly ILogger<TManager> _logger;
    private readonly ILSCoreDbContext? _dbContext;

    public LSCoreContextUser? CurrentUser { get; }
        
    protected LSCoreManagerBase(ILogger<TManager> logger)
    {
        _logger = logger;
        _dbContext = null;
    }

    protected LSCoreManagerBase(ILogger<TManager> logger, ILSCoreDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    protected LSCoreManagerBase(ILogger<TManager> logger, LSCoreContextUser currentUser)
    {
        _logger = logger;
        _dbContext = null;
        CurrentUser = currentUser;
    }

    protected LSCoreManagerBase(ILogger<TManager> logger, ILSCoreDbContext dbContext, LSCoreContextUser currentUser)
    {
        _logger = logger;
        _dbContext = dbContext;
        CurrentUser = currentUser;
    }

    /// <summary>
    /// Adds or updates entity to database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected TEntity Save<TEntity, TRequest>(TRequest request)
        where TEntity : class, ILSCoreEntity, new()
        where TRequest : LSCoreSaveRequest
    {
        try
        {
            if (_dbContext == null)
                throw new LSCoreBadRequestException(
                    string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

            request.Validate();

            var entityMapper = (ILSCoreDtoMapper<TEntity, TRequest>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(ILSCoreDtoMapper<TEntity, TRequest>));

            var entity = new TEntity();
            if (!request.Id.HasValue)
            {
                var lastId = _dbContext.Set<TEntity>()
                    .AsQueryable()
                    .OrderByDescending(x => x.Id)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                if (entityMapper == null)
                    entity.InjectFrom(request);
                else
                    entity.ToDto<TEntity, TRequest>();

                entity.Id = ++lastId;
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = CurrentUser?.Id ?? 0;

                _dbContext.Set<TEntity>()
                    .Add(entity);
            }
            else
            {
                entity = _dbContext.Set<TEntity>()
                    .FirstOrDefault(x => x.Id == request.Id);

                if (entity == null)
                    throw new LSCoreNotFoundException();

                if (entityMapper == null)
                    entity.InjectFrom(request);
                else
                    entity.ToDto<TEntity, TRequest>();

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = CurrentUser?.Id ?? 0;

                _dbContext.Set<TEntity>()
                    .Update(entity);
            }
            _dbContext.SaveChanges();

            return entity;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Updates entity into database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected TEntity Update<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            if (_dbContext == null)
                throw new LSCoreBadRequestException(
                    string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

            _dbContext.Set<TEntity>()
                .Update(entity);

            _dbContext.SaveChanges();

            return entity;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Inserts entity into database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    protected TEntity InsertNonLSCoreEntity<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            if (_dbContext == null)
                throw new LSCoreBadRequestException(
                    string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

            _dbContext.Set<TEntity>()
                .Add(entity);

            _dbContext.SaveChanges();

            return entity;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Inserts LSCoreEntity into database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <param name="assignCreatedBy"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public TEntity Insert<TEntity>(TEntity entity, bool assignCreatedBy = true) where TEntity : class, ILSCoreEntity
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.IsActive = true;
            
        if(assignCreatedBy)
            entity.CreatedBy = CurrentUser?.Id ?? 0;

        return InsertNonLSCoreEntity(entity);
    }
        
    /// <summary>
    /// Inserts LSCoreEntity list into database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="assignCreatedBy"></param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public List<TEntity> Insert<TEntity>(List<TEntity> entities, bool assignCreatedBy = true) where TEntity : class, ILSCoreEntity
    {
        entities.ForEach(x =>
        {
            x.CreatedAt = DateTime.UtcNow;
            x.IsActive = true;
                
            if(assignCreatedBy)
                x.CreatedBy = CurrentUser?.Id ?? 0;
        });

        _dbContext!
            .Set<TEntity>()
            .AddRange(entities);
            
        _dbContext.SaveChanges();
        return entities;
    }

    /// <summary>
    /// Gets T entity table as queryable
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    protected IQueryable<TEntity> Queryable<TEntity>()
        where TEntity : class, ILSCoreEntity
    {
        try
        {
            if (_dbContext == null)
                throw new LSCoreBadRequestException(
                    string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

            var query = _dbContext.Set<TEntity>()
                .AsQueryable();

            return query;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Deletes record from the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    protected void HardDelete<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext!
            .Set<TEntity>()
            .Remove(entity);

        _dbContext.SaveChanges();
    }
        
    /// <summary>
    /// Deletes records from the database
    /// </summary>
    /// <param name="entities"></param>
    /// <typeparam name="TEntity"></typeparam>
    protected void HardDelete<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        _dbContext!
            .Set<TEntity>()
            .RemoveRange(entities);

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Deletes record from the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    protected void HardDelete<TEntity>(long id)
        where TEntity : class, ILSCoreEntityBase
    {
        var entity = _dbContext?
            .Set<TEntity>()
            .FirstOrDefault(x => x.Id == id);

        if (entity == null)
            throw new LSCoreNotFoundException();

        HardDelete(entity);
    }

    /// <summary>
    /// Updates records is_active to false
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    public void SoftDelete<TEntity>(TEntity entity) where TEntity : class, ILSCoreEntity
    {
        entity.IsActive = false;
        Update(entity);
    }
        
    /// <summary>
    /// Updates records is_active to true
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="TEntity"></typeparam>
    public void Restore<TEntity>(TEntity entity) where TEntity : class, ILSCoreEntity
    {
        entity.IsActive = true;
        Update(entity);
    }
}

public class LSCoreManagerBase<TManager, TEntity> : LSCoreManagerBase<TManager>
    where TEntity : class, ILSCoreEntity, new()
{
    public LSCoreManagerBase(ILogger<TManager> logger, ILSCoreDbContext dbContext)
        : base(logger, dbContext) { }
    public LSCoreManagerBase(ILogger<TManager> logger, ILSCoreDbContext dbContext, LSCoreContextUser currentUser)
        : base(logger, dbContext, currentUser) { }
    
    /// <summary>
    /// Adds or save entity to database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public TEntity Save<TRequest>(TRequest request)
        where TRequest : LSCoreSaveRequest =>
        Save<TEntity, TRequest>(request);

    /// <summary>
    /// Adds or save entity to database
    /// </summary>
    /// <param name="request"></param>
    /// <param name="responseMapper"></param>
    /// <returns></returns>
    public TPayload Save<TRequest, TPayload>(TRequest request, Func<TEntity, TPayload> responseMapper)
        where TRequest : LSCoreSaveRequest =>
        responseMapper(base.Save<TEntity, TRequest>(request));

    /// <summary>
    /// Gets manager entity table as queryable
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> Queryable() =>
        Queryable<TEntity>();

    /// <summary>
    /// Deletes record from the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    public void HardDelete(TEntity entity) =>
        base.HardDelete(entity);

    /// <summary>
    /// Deletes record from the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    public void HardDelete(long id) =>
        base.HardDelete<TEntity>(id);
}