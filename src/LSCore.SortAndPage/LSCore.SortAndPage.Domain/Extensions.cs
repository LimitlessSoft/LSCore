using System.Linq.Expressions;
using LSCore.SortAndPage.Contracts;

namespace LSCore.SortAndPage.Domain;

public static class Extensions
{
	public static LSCoreSortedAndPagedResponse<TPayload> ToSortedAndPagedResponse<
		TPayload,
		TSortColumn
	>(
		this IQueryable<TPayload> source,
		LSCoreSortableAndPageableRequest<TSortColumn> request,
		Dictionary<TSortColumn, Expression<Func<TPayload, object>>> sortRules
	)
		where TSortColumn : struct =>
		new()
		{
			Pagination = new LSCorePaginationData(
				request.CurrentPage,
				request.PageSize,
				source.Count()
			),
			Payload = source.SortAndPageQuery(request, sortRules).ToList()
		};
}
