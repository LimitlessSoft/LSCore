using LSCore.Contracts.Requests;
using LSCore.Contracts.Responses;

namespace LSCore.Contracts.Extensions
{
    public static class LSCoreQueriableExtensions
    {
        public static LSCorePagedResponse<TEntity> ToPagedResponse<TEntity>(this IQueryable<TEntity> source, LSCoreSortablePageableRequest request) =>
            new LSCorePagedResponse<TEntity>(source.Skip(request.CurrentPage - 1 * request.PageSize).Take(request.PageSize).ToList());
    }
}
