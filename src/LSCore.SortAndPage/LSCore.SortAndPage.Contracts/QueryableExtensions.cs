using System.Linq.Expressions;

namespace LSCore.SortAndPage.Contracts;

public static class QueryableExtensions
{
	public static IQueryable<T> SortQuery<T, TSortColumn>(
		this IQueryable<T> source,
		LSCoreSortableRequest<TSortColumn> request,
		Dictionary<TSortColumn, Expression<Func<T, object>>> sortDictionary
	)
		where TSortColumn : struct
	{
		if (!request.SortColumn.HasValue)
			return source;

		source =
			request.SortDirection == System.ComponentModel.ListSortDirection.Descending
				? source.OrderByDescending(sortDictionary[request.SortColumn.Value])
				: source.OrderBy(sortDictionary[request.SortColumn.Value]);

		return source;
	}

	public static IQueryable<T> SortAndPageQuery<T, TSortColumn>(
		this IQueryable<T> source,
		LSCoreSortableAndPageableRequest<TSortColumn> request,
		Dictionary<TSortColumn, Expression<Func<T, object>>> sortRules
	)
		where TSortColumn : struct =>
		source
			.SortQuery(request, sortRules)
			.Skip((request.CurrentPage - 1) * request.PageSize)
			.Take(request.PageSize);
}
