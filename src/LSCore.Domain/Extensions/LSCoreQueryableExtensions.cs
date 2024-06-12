using LSCore.Contracts.Extensions;
using LSCore.Contracts.Responses;
using LSCore.Contracts.Requests;
using System.Linq.Expressions;

namespace LSCore.Domain.Extensions;

public static class LSCoreQueryableExtensions
{
    public static LSCoreSortedAndPagedResponse<TEntity> ToSortedAndPagedResponse<TEntity, TSortColumn>(
        this IQueryable<TEntity> source,
        LSCoreSortableAndPageableRequest<TSortColumn> request,
        Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortRules)
        where TSortColumn : struct =>
            new ()
            {
                Pagination = new LSCoreSortedAndPagedResponse<TEntity>.PaginationData(request.CurrentPage, request.PageSize, source.Count()),
                Payload = source.SortAndPageQuery(request, sortRules).ToList()
            };
    
    public static LSCoreSortedAndPagedResponse<TPayloadDto> ToSortedAndPagedResponse<TEntity, TSortColumn, TPayloadDto>(
        this IQueryable<TEntity> source,
        LSCoreSortableAndPageableRequest<TSortColumn> request,
        Dictionary<TSortColumn, Expression<Func<TEntity, object>>> sortRules)
        where TSortColumn : struct
        where TEntity : class =>
            new ()
            {
                Pagination = new LSCoreSortedAndPagedResponse<TPayloadDto>.PaginationData(request.CurrentPage, request.PageSize, source.Count()),
                Payload = source.SortAndPageQuery(request, sortRules).ToDtoList<TEntity, TPayloadDto>()
            };
}