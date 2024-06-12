using LSCore.Contracts.Requests;
using System.Linq.Expressions;

// ReSharper disable MemberCanBePrivate.Global

namespace LSCore.Contracts.Extensions;

public static class LSCoreQueryableExtensions
{
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
        LSCoreSortableRequest<TSortColumn> request,
        Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortDictionary)
        where TSortColumn : struct
    {
        if (!request.SortColumn.HasValue)
            return source;
        
        source = request.SortDirection == System.ComponentModel.ListSortDirection.Descending
            ? source.OrderByDescending(sortDictionary[request.SortColumn.Value])
            : source.OrderBy(sortDictionary[request.SortColumn.Value]);

        return source;
    }

    public static IQueryable<TEntity> SortAndPageQuery<TEntity, TSortColumn>(this IQueryable<TEntity> source,
        LSCoreSortableAndPageableRequest<TSortColumn> request,
        Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortRules)
        where TSortColumn : struct =>
            source
                .SortQuery(request, sortRules)
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize);
}