using static LSCore.Contracts.Extensions.LSCoreHttpResponseExtensions;
using LSCore.Contracts.Enums.ValidationCodes;
using LSCore.Contracts.Http.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Extensions;
using LSCore.Contracts.IManagers;
using Microsoft.AspNetCore.Http;
using LSCore.Contracts.Entities;
using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using LSCore.Domain.Validators;
using System.Linq.Expressions;
using LSCore.Contracts.Http;
using Omu.ValueInjecter;
using FluentValidation;
using LSCore.Contracts;

namespace LSCore.Domain.Managers
{
    public class LSCoreBaseManager<TManager> : ILSCoreBaseManager
    {
        private readonly ILogger<TManager> _logger;
        private readonly DbContext? _dbContext;

        public LSCoreContextUser? CurrentUser { get; private set; }

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
            if (!httpContext.User.Identity!.IsAuthenticated)
                return;

            var claims = httpContext.User?.Claims.ToList();
            if (claims == null)
                return;

            CurrentUser = new LSCoreContextUser();
            CurrentUser.Username = claims.FirstOrDefault(x => x.Type == LSCoreContractsConstants.ClaimNames.CustomUsername)?.Value.ToString() ?? LSCoreContractsConstants.UndefinedContextUsername;
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
            try
            {
                if (_dbContext == null)
                    return LSCoreResponse<TEntity>.BadRequest(string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

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
                _dbContext.SaveChanges();

                return new LSCoreResponse<TEntity>(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return LSCoreResponse<TEntity>.InternalServerError();
            }
        }

        /// <summary>
        /// Updates entity into database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LSCoreResponse<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                if (_dbContext == null)
                    return LSCoreResponse<TEntity>.BadRequest(string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

                _dbContext.Set<TEntity>()
                    .Update(entity);

                _dbContext.SaveChanges();

                return new LSCoreResponse<TEntity>(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return LSCoreResponse<TEntity>.InternalServerError();
            }
        }

        /// <summary>
        /// Inserts entity into database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public LSCoreResponse<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                if (_dbContext == null)
                    return LSCoreResponse<TEntity>.BadRequest(string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

                _dbContext.Set<TEntity>()
                    .Add(entity);

                _dbContext.SaveChanges();

                return new LSCoreResponse<TEntity>(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return LSCoreResponse<TEntity>.InternalServerError();
            }
        }

        /// <summary>
        /// Gets T entity table as queryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public LSCoreResponse<IQueryable<TEntity>> Queryable<TEntity>()
            where TEntity : class, ILSCoreEntity
        {
            try
            {
                if (_dbContext == null)
                    return LSCoreResponse<IQueryable<TEntity>>.BadRequest(string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

                var query = _dbContext.Set<TEntity>()
                    .AsQueryable();

                return new LSCoreResponse<IQueryable<TEntity>>(query);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return LSCoreResponse<IQueryable<TEntity>>.InternalServerError();
            }
        }

        /// <summary>
        /// Gets first T entity
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public LSCoreResponse<TEntity> First<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ILSCoreEntity
        {
            try
            {
                if (_dbContext == null)
                    return LSCoreResponse<TEntity>.BadRequest(string.Format(LSCoreCommonValidationCodes.COMM_006.GetDescription()!, nameof(_dbContext)));

                var response = new LSCoreResponse<TEntity>();

                var querableResponse = Queryable<TEntity>();
                response.Merge(querableResponse);
                if (response.NotOk)
                    return response;

                var entity = querableResponse.Payload!.FirstOrDefault(predicate);
                if (entity == null)
                    return LSCoreResponse<TEntity>.NotFound();

                return new LSCoreResponse<TEntity>(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return LSCoreResponse<TEntity>.InternalServerError();
            }
        }

        public LSCoreResponse<TPayload> First<TEntity, TPayload>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, ILSCoreEntity
        {
            var response = new LSCoreResponse<TPayload>();
            var entityResponse = First(predicate);

            response.Merge(entityResponse);
            if (response.NotOk)
                return response;

            response.Payload = entityResponse.Payload!.ToDto<TPayload, TEntity>();
            return response;
        }

        /// <summary>
        /// Deletes record from the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public LSCoreResponse HardDelete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext!
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

        public ILSCoreResponse<TPayload> ExecuteCustomQuery<TPayload>()
            where TPayload : class
        {
            var query = (ILSCoreQuery<TPayload>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(ILSCoreQuery<TPayload>));
            if (query == null)
                throw new NullReferenceException(nameof(query));

            if (_dbContext == null)
                throw new NullReferenceException(nameof(_dbContext));

            return query.Execute((ILSCoreDbContext)_dbContext);
        }

        public ILSCoreResponse<TPayload> ExecuteCustomQuery<TRequest, TPayload>(TRequest request)
            where TPayload : class
        {
            var query = (ILSCoreQuery<TRequest, TPayload>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(ILSCoreQuery<TRequest, TPayload>));
            if (query == null)
                throw new NullReferenceException(nameof(query));

            if (_dbContext == null)
                throw new NullReferenceException(nameof(_dbContext));

            if(request == null)
                throw new NullReferenceException(nameof(request));

            query.Request = request;

            return query.Execute((ILSCoreDbContext)_dbContext);
        }

        public ILSCoreResponse ExecuteCustomCommand(ILSCoreCommand command)
        {
            if (command == null)
                throw new NullReferenceException(nameof(command));

            if (_dbContext == null)
                throw new NullReferenceException(nameof(_dbContext));

            return command.Execute((ILSCoreDbContext)_dbContext);
        }

        public ILSCoreResponse ExecuteCustomCommand<TRequest>(TRequest request)
            where TRequest : class
        {
            var command = (ILSCoreCommand<TRequest>?)LSCoreDomainConstants.Container?.TryGetInstance(typeof(ILSCoreCommand<TRequest>));
            if (command == null)
                throw new NullReferenceException(nameof(command));

            if (_dbContext == null)
                throw new NullReferenceException(nameof(_dbContext));

            if (request == null)
                throw new NullReferenceException(nameof(request));

            command.Request = request;

            return command.Execute((ILSCoreDbContext)_dbContext);
        }
    }

    public class LSCoreBaseManager<TManager, TEntity> : LSCoreBaseManager<TManager>
        where TEntity : LSCoreEntity, ILSCoreEntity, new()
    {
        private readonly ILogger<TManager> _logger;
        private readonly DbContext _dbContext;

        public LSCoreBaseManager(ILogger<TManager> logger, DbContext dbContext)
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

            return responseMapper(saveResponse.Payload!);
        }

        /// <summary>
        /// Gets manager entity table as queryable
        /// </summary>
        /// <returns></returns>
        public LSCoreResponse<IQueryable<TEntity>> Queryable() =>
            Queryable<TEntity>();

        /// <summary>
        /// Gets manager entity table as queryable
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use Queryable(LSCoreQueryableOptions<TEntity>? options) instead")]
        public LSCoreResponse<IQueryable<TEntity>> Queryable(Expression<Func<TEntity, bool>> predicate)
        {
            var response = new LSCoreResponse<IQueryable<TEntity>>();

            var querableResponse = Queryable<TEntity>();
            response.Merge(querableResponse);
            if (response.NotOk)
                return response;
            
            response.Payload = querableResponse.Payload!.Where(predicate);
            return response;
        }

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
