namespace LSCore.Contracts.Responses;

public partial class LSCoreSortedAndPagedResponse<TPayload>
{
    public record PaginationData(int Page, int PageSize, int TotalCount)
    {
        public int TotalPages => TotalCount / PageSize + (TotalCount % PageSize == 0 ? 0 : 1);
    }
}