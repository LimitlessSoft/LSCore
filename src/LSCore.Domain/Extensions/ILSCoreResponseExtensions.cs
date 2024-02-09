using LSCore.Contracts.Http.Interfaces;
using Microsoft.EntityFrameworkCore;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Entities;
using LSCore.Domain.Extensions;
using LSCore.Contracts.Http;
using System.Linq.Expressions;
using LSCore.Domain;
using LSCore.Contracts.Responses;
using LSCore.Contracts.Requests;

namespace LSCore.Contracts.Extensions
{
    public static class ILSCoreResponseExtensions
    {

        /// <summary>
        /// Converts the response of the queryable to a LSCoreListResponse
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryableResponse"></param>
        /// <returns></returns>
        public static LSCoreListResponse<TEntity> ToLSCoreListResponse<TEntity>(this ILSCoreResponse<IQueryable<TEntity>> queryableResponse)
            where TEntity : LSCoreEntity
        {
            var response = new LSCoreListResponse<TEntity>();

            queryableResponse.Merge(response);
            if (response.NotOk)
                return response;

            response.Payload = queryableResponse.Payload!.ToList();
            return response;
        }

        /// <summary>
        /// Converts the response of the queryable to a LSCoreListResponse with specified DTO using LSCoreDtoMapper
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryableResponse"></param>
        /// <returns></returns>
        public static LSCoreListResponse<TDto> ToLSCoreListResponse<TDto, TEntity>(this ILSCoreResponse<IQueryable<TEntity>> queryableResponse)
            where TEntity : LSCoreEntity
        {
            var response = new LSCoreListResponse<TDto>();

            queryableResponse.Merge(response);
            if (response.NotOk)
                return response;

            response.Payload = EntityExtensions.ToDtoList<TDto, TEntity>(queryableResponse.Payload!);
            return response;
        }

        /// <summary>
        /// Sorts the queryable using the specified sortDictionary
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TSortColumn"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <param name="sortDictionary"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> SortQuery<TEntity, TSortColumn>(this IQueryable<TEntity> source,
            LSCoreSortableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TSortColumn : struct
        {
            if (request.SortColumn.HasValue)
            {
                if (request.SortDirection == System.ComponentModel.ListSortDirection.Descending)
                    source = source.OrderByDescending(sortDictionary[request.SortColumn.Value]);
                else
                    source = source.OrderBy(sortDictionary[request.SortColumn.Value]);
            }

            return source;
        }

        /// <summary>
        /// Converts the response of the queryable to a LSCoreSortedPagedResponse using LSCoreSortablePageableRequest for sorting and paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TSortColumn"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <param name="sortDictionary"></param>
        /// <returns></returns>
        public static LSCoreSortedPagedResponse<TEntity> ToSortedAndPagedResponse<TEntity, TSortColumn>(this IQueryable<TEntity> source,
            LSCoreSortablePageableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TSortColumn : struct
        {
            source = SortQuery(source, request, sortDictionary);

            var pagedResult = source.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            return new LSCoreSortedPagedResponse<TEntity>(pagedResult.ToList(), request, source.Count());
        }

        /// <summary>
        /// Converts the response of the queryable to a LSCoreSortedPagedResponse with specified DTO using LSCoreDtoMapper and LSCoreSortablePageableRequest for sorting and paging
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TSortColumn"></typeparam>
        /// <param name="queryableResponse"></param>
        /// <param name="request"></param>
        /// <param name="sortDictionary"></param>
        /// <returns></returns>
        public static LSCoreSortedPagedResponse<TDto> ToLSCoreSortedPagedResponse<TDto, TEntity, TSortColumn>(this ILSCoreResponse<IQueryable<TEntity>> queryableResponse,
            LSCoreSortablePageableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TEntity : LSCoreEntity
            where TSortColumn : struct
        {
            var response = new LSCoreSortedPagedResponse<TDto>();
            
            response.Merge(queryableResponse);
            if (queryableResponse.NotOk)
                return response;

            var source = queryableResponse.Payload!;
            source = SortQuery(source, request, sortDictionary);

            var pagedResult = source.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            return new LSCoreSortedPagedResponse<TDto>(EntityExtensions.ToDtoList<TDto, TEntity>(pagedResult), request, source.Count());
        }

        /// <summary>
        /// Appends the filters to the queryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static LSCoreResponse<IQueryable<TEntity>> LSCoreFilters<TEntity, TLSCoreFilter>(this LSCoreResponse<IQueryable<TEntity>> source)
            where TEntity : LSCoreEntity
            where TLSCoreFilter : ILSCoreFilter<TEntity>
        {
            var filters = LSCoreDomainConstants.Container!.TryGetInstance<TLSCoreFilter>();
            if (filters == null)
                throw new NullReferenceException(nameof(filters));

            return source.LSCoreFilters(filters.FiltersExpressions.ToArray());
        }
        /// <summary>
        /// Appends the filters to the queryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static LSCoreResponse<IQueryable<TEntity>> LSCoreFilters<TEntity>(this LSCoreResponse<IQueryable<TEntity>> source, params Expression<Func<TEntity, bool>>[] filters)
            where TEntity : LSCoreEntity
        {
            if (source.NotOk)
                return source;

            foreach (var filterExpression in filters)
                source.Payload = source.Payload!.Where(filterExpression);

            return source;
        }

        /// <summary>
        /// Appends the includes to the queryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static LSCoreResponse<IQueryable<TEntity>> LSCoreIncludes<TEntity, TLSCoreIncludes>(this LSCoreResponse<IQueryable<TEntity>> source)
            where TEntity : LSCoreEntity
            where TLSCoreIncludes : ILSCoreIncludes<TEntity>
        {
            var includes = LSCoreDomainConstants.Container!.TryGetInstance<TLSCoreIncludes>();
            if (includes == null)
                throw new NullReferenceException(nameof(includes));

            return source.LSCoreIncludes(includes.IncludesExpressions.ToArray());
        }
        /// <summary>
        /// Appends the includes to the queryable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static LSCoreResponse<IQueryable<TEntity>> LSCoreIncludes<TEntity>(this LSCoreResponse<IQueryable<TEntity>> source, params Expression<Func<TEntity, dynamic?>>[] includes)
            where TEntity : LSCoreEntity
        {
            if (source.NotOk)
                return source;

            foreach (var includeExpression in includes)
                source.Payload = source.Payload!.Include(includeExpression);

            return source;
        }
    }
}
