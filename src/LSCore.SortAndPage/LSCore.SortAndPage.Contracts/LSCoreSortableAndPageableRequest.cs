namespace LSCore.SortAndPage.Contracts;

public class LSCoreSortableAndPageableRequest<TSortColumn> : LSCoreSortableRequest<TSortColumn>
	where TSortColumn : struct
{
	public int PageSize { get; set; } = 10;
	public int CurrentPage { get; set; } = 1;
}
