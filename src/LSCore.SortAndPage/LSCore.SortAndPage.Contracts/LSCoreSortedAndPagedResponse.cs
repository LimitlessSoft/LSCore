namespace LSCore.SortAndPage.Contracts;

public class LSCoreSortedAndPagedResponse<TPayload>
{
	public List<TPayload>? Payload { get; set; }
	public LSCorePaginationData? Pagination { get; set; }
}
