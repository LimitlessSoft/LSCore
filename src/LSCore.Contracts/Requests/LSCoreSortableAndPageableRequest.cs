using LSCore.Contracts.Interfaces;

namespace LSCore.Contracts.Requests;

public abstract class LSCoreSortableAndPageableRequest<TSortColumn>
    : LSCoreSortableRequest<TSortColumn>, ILSCorePageable
    where TSortColumn : struct
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; set; } = 1;
}