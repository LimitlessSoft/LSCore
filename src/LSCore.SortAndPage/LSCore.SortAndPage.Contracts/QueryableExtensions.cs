using System.ComponentModel;

namespace LSCore.SortAndPage.Contracts;

public static class QueryableExtensions
{
	public static IQueryable<T> SortQuery<T, TSortColumn>(
		this IQueryable<T> source,
		LSCoreSortableRequest<TSortColumn> request,
		Dictionary<TSortColumn, LSCoreSortRule<T>> sortRules
	)
		where TSortColumn : struct
		where T : class
	{
		if (!request.SortColumn.HasValue)
			return source;

		source =
			request.SortDirection == ListSortDirection.Descending
				? source.OrderByDescending(sortRules[request.SortColumn.Value].SortExpression)
				: source.OrderBy(sortRules[request.SortColumn.Value].SortExpression);

		return source;
	}

	public static IQueryable<T> SortAndPageQuery<T, TSortColumn>(
		this IQueryable<T> source,
		LSCoreSortableAndPageableRequest<TSortColumn> request,
		Dictionary<TSortColumn, LSCoreSortRule<T>> sortRules
	)
		where TSortColumn : struct
		where T : class =>
		source
			.SortQuery(request, sortRules)
			.Skip((request.CurrentPage - 1) * request.PageSize)
			.Take(request.PageSize);
}
