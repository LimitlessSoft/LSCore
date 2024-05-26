using LSCore.Contracts.Enums.ValidationCodes;
using Microsoft.Extensions.Logging;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Extensions;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using Microsoft.AspNetCore.Http;
using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using Omu.ValueInjecter;
using LSCore.Contracts;

// ReSharper disable MemberCanBePrivate.Global

namespace LSCore.Domain.Managers
{
    public abstract class LSCoreManagerBase<TManager>
    {
        private readonly ILogger<TManager> _logger;
        private readonly ILSCoreDbContext? _dbContext;

        public LSCoreContextUser? CurrentUser { get; private set; }

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

        public void SetContext(HttpContext httpContext)
        {
            if (!httpContext.User.Identity!.IsAuthenticated)
                return;

            var claims = httpContext.User?.Claims.ToList();
            if (claims == null)
                return;

            CurrentUser = new LSCoreContextUser
            {
                Username = claims.FirstOrDefault(x => x.Type == LSCoreContractsConstants.ClaimNames.CustomUsername)?.Value.ToString() ?? LSCoreContractsConstants.UndefinedContextUsername,
                Id = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == LSCoreContractsConstants.ClaimNames.CustomUserId)?.Value)
            };
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
            
            if(assignCreatedBy)
                entity.CreatedBy = CurrentUser?.Id ?? 0;

            return InsertNonLSCoreEntity(entity);
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
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        protected void HardDelete<TEntity>(int id)
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
    }

    public class LSCoreManagerBase<TManager, TEntity> (ILogger<TManager> logger, ILSCoreDbContext dbContext)
        : LSCoreManagerBase<TManager>(logger, dbContext)
        where TEntity : class, ILSCoreEntity, new()
    {
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
        public void HardDelete(int id) =>
            base.HardDelete<TEntity>(id);
    }
}
