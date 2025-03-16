namespace LSCore.SortAndPage.Contracts;

public record LSCorePaginationData(int Page, int PageSize, int TotalCount)
{
	public int TotalPages => TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1);
}
