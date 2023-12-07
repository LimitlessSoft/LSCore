using LSCore.Contracts.Requests;
using LSCore.Contracts.Responses;
using System.Linq.Expressions;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreQueriableExtensions
    {
        public static LSCoreSortedPagedResponse<TEntity> ToSortedAndPagedResponse<TEntity, TSortColumn>(this IQueryable<TEntity> source,
            LSCoreSortablePageableRequest<TSortColumn> request, Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
            where TSortColumn : struct
        {
            if(request.SortColumn.HasValue)
            {
                if(request.SortDirection == System.ComponentModel.ListSortDirection.Descending)
                    source = source.OrderByDescending(sortDictionary[request.SortColumn.Value]);
                else
                    source = source.OrderBy(sortDictionary[request.SortColumn.Value]);
            }

            var pagedResult = source.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);

            return new LSCoreSortedPagedResponse<TEntity>(pagedResult.ToList(), request, source.Count());
        }
    }
}
