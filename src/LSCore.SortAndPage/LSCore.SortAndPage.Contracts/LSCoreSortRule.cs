using System.Linq.Expressions;

namespace LSCore.SortAndPage.Contracts;

public class LSCoreSortRule<T>(Expression<Func<T, object>> sortExpression)
	where T : class
{
	public Expression<Func<T, object>> SortExpression { get; } = sortExpression;
}
