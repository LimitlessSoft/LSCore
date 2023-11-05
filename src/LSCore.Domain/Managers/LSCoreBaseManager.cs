using LSCore.Contracts;
using LSCore.Contracts.Http;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using LSCore.Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System.Linq.Expressions;
using static LSCore.Contracts.Extensions.LSCoreHttpResponseExtensions;

namespace LSCore.Domain.Managers
{
    public class LSCoreBaseManager<TManager> : ILSCoreBaseManager
    {
        private readonly ILogger<TManager> _logger;
        private readonly DbContext? _dbContext;

        public LSCoreContextUser CurrentUser { get; set; }

        public LSCoreBaseManager(ILogger<TManager> logger)
        {
            _logger = logger;
            _dbContext = null;
        }

        public LSCoreBaseManager(ILogger<TManager> logger, DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void SetContext(HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
                return;

            var claims = httpContext.User?.Claims.ToList();
            if (claims == null)
                return;

            CurrentUser = new LSCoreContextUser();
            CurrentUser.Username = claims.FirstOrDefault(x => x.Type == LSCoreContractsConstants.ClaimNames.CustomUsername)?.Value.ToString() ?? "UNDEFINED";
            CurrentUser.Id = Convert.ToInt32(claims.FirstOrDefault(x => x.Type == LSCoreContractsConstants.ClaimNames.CustomUserId)?.Value);
        }

        /// <summary>
        /// Adds or updates entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LSCoreResponse<TEntity> Save<TEntity, TRequest>(TRequest request)
            where TEntity : class, ILSCoreEntity, new()
            where TRequest : LSCoreSaveRequest
        {
            if (_dbContext == null)
                return LSCoreResponse<TEntity>.InternalServerError();

            var response = new LSCoreResponse<TEntity>();
            if (request.IsRequestInvalid(response))
                return response;

            var entityMapper = (ILSCoreMap<TEntity, TRequest>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(ILSCoreMap<TEntity, TRequest>));

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
                    entityMapper.Map(entity, request);

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
                    return LSCoreResponse<TEntity>.NotFound();

                if (entityMapper == null)
                    entity.InjectFrom(request);
                else
                    entityMapper.Map(entity, request);

                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = CurrentUser?.Id ?? 0;

                _dbContext.Set<TEntity>()
                    .Update(entity);
            }
            try
            {
                _dbContext.SaveChanges();
            }
            catch
            {
                return LSCoreResponse<TEntity>.InternalServerError();
            }

            return new LSCoreResponse<TEntity>(entity);
        }

        /// <summary>
        /// Updates entity into database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext));

            _dbContext.Set<TEntity>()
                .Update(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Inserts entity into database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class
        {
            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext));

            _dbContext.Set<TEntity>()
                .Add(entity);

            _dbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Gets T entity table as queryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Queryable<T>() where T : class
        {
            if (_dbContext == null)
                throw new ArgumentNullException(nameof(_dbContext));

            return _dbContext.Set<T>()
                .AsQueryable();
        }

        /// <summary>
        /// Gets first T entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public LSCoreResponse<T> First<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (_dbContext == null)
                return LSCoreResponse<T>.InternalServerError();

            var entity = Queryable<T>().FirstOrDefault(predicate);
            if (entity == null)
                return LSCoreResponse<T>.NotFound();

            return new LSCoreResponse<T>(entity);
        }

        public LSCoreResponse<TPayload> First<TEntity, TPayload>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            var response = new LSCoreResponse<TPayload>();
            var entityResponse = First(predicate);

            response.Merge(entityResponse);
            if (response.NotOk)
                return response;

            response.Payload = entityResponse.Payload.ToDto<TPayload, TEntity>();
            return response;
        }

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public LSCoreResponse HardDelete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext
                .Set<TEntity>()
                .Remove(entity);

            _dbContext.SaveChanges();

            return new LSCoreResponse();
        }

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public LSCoreResponse HardDelete<TEntity>(int id)
            where TEntity : class, ILSCoreEntityBase
        {
            var entity = _dbContext?
                .Set<TEntity>()
                .FirstOrDefault(x => x.Id == id);

            if (entity == null)
                return LSCoreResponse.NotFound();

            return HardDelete(entity);
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

    public class BaseManager<TManager, TEntity> : LSCoreBaseManager<TManager> where TEntity
        : class, ILSCoreEntity, new()
    {
        private readonly ILogger<TManager> _logger;
        private readonly DbContext _dbContext;

        public BaseManager(ILogger<TManager> logger, DbContext dbContext)
            : base(logger, dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds or save entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LSCoreResponse<TEntity> Save<TRequest>(TRequest request)
            where TRequest : LSCoreSaveRequest =>
            Save<TEntity, TRequest>(request);

        /// <summary>
        /// Adds or save entity to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LSCoreResponse<TPayload> Save<TRequest, TPayload>(TRequest request, Func<TEntity, LSCoreResponse<TPayload>> responseMapper)
            where TRequest : LSCoreSaveRequest
        {
            var response = new LSCoreResponse<TPayload>();

            var saveResponse = base.Save<TEntity, TRequest>(request);
            response.Merge(saveResponse);
            if (response.NotOk)
                return response;

            return responseMapper(saveResponse.Payload);
        }

        /// <summary>
        /// Gets manager entity table as queryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Queryable() =>
            Queryable<TEntity>();

        /// <summary>
        /// Gets manager entity table as queryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate) =>
            Queryable<TEntity>()
                .Where(predicate);

        /// <summary>
        /// Gets first manager entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public LSCoreResponse<TEntity> First(Expression<Func<TEntity, bool>> predicate) =>
            base.First<TEntity>(predicate);

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public LSCoreResponse HardDelete(TEntity entity) =>
            base.HardDelete(entity);

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public LSCoreResponse HardDelete(int id) =>
            base.HardDelete<TEntity>(id);
    }
}
