using LSCore.SortAndPage.Contracts;
using Sample.SortAndPage.Api.Entities;
using Sample.SortAndPage.Api.SortColumnCodes;

namespace Sample.SortAndPage.Api;

public static class SortColumnRules
{
	public static Dictionary<
		ProductsSortColumn,
		LSCoreSortRule<ProductEntity>
	> ProductsSortColumnCodesRules =
		new()
		{
			{ ProductsSortColumn.Id, new LSCoreSortRule<ProductEntity>(x => x.Id) },
			{ ProductsSortColumn.Name, new LSCoreSortRule<ProductEntity>(x => x.Name) }
		};
}
