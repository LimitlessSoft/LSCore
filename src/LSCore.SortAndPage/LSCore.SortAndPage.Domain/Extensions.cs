using LSCore.SortAndPage.Contracts;

namespace LSCore.SortAndPage.Domain;

public static class Extensions
{
	public static LSCoreSortedAndPagedResponse<T> ToSortedAndPagedResponse<T, TSortColumn>(
		this IQueryable<T> source,
		LSCoreSortableAndPageableRequest<TSortColumn> request,
		Dictionary<TSortColumn, LSCoreSortRule<T>> sortRules
	)
		where TSortColumn : struct
		where T : class =>
		new()
		{
			Pagination = new LSCorePaginationData(
				request.CurrentPage,
				request.PageSize,
				source.Count()
			),
			Payload = source.SortAndPageQuery(request, sortRules).ToList()
		};

	public static LSCoreSortedAndPagedResponse<TPayload> ToSortedAndPagedResponse<
		T,
		TSortColumn,
		TPayload
	>(
		this IQueryable<T> source,
		LSCoreSortableAndPageableRequest<TSortColumn> request,
		Dictionary<TSortColumn, LSCoreSortRule<T>> sortRules,
		Func<T, TPayload> mapFunc
	)
		where TSortColumn : struct
		where T : class =>
		new()
		{
			Pagination = new LSCorePaginationData(
				request.CurrentPage,
				request.PageSize,
				source.Count()
			),
			Payload = source
				.SortAndPageQuery(request, sortRules)
				.AsEnumerable()
				.Select(mapFunc)
				.ToList()
		};
}
