namespace LSCore.Contracts.Responses;

public partial class LSCoreSortedAndPagedResponse<TPayload>
{
    public List<TPayload>? Payload { get; set; }
    public PaginationData? Pagination { get; set; }
}