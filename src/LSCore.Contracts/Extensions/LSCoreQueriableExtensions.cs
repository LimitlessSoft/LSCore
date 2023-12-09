using LSCore.Contracts.Requests;
using LSCore.Contracts.Responses;
using System.Linq.Expressions;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreQueriableExtensions
    {
        private static IQueryable<TEntity> SortQuery<TEntity, TSortColumn>(this IQueryable<TEntity> source,
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
        public static LSCoreSortedListResponse<TEntity> ToSortedListResponse<TEntity, TSortColumn>(this IQueryable<TEntity> source,
            LSCoreSortableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TSortColumn : struct
        {
            source = SortQuery(source, request, sortDictionary);

            return new LSCoreSortedListResponse<TEntity>(source.ToList());
        }

        public static LSCoreSortedPagedResponse<TEntity> ToSortedAndPagedResponse<TEntity, TSortColumn>(this IQueryable<TEntity> source,
            LSCoreSortablePageableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TSortColumn : struct
        {
            source = SortQuery(source, request, sortDictionary);

            var pagedResult = source.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            return new LSCoreSortedPagedResponse<TEntity>(pagedResult.ToList(), request, source.Count());
        }
    }
}
