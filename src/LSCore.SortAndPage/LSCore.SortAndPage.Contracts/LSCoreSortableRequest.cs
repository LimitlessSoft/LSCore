using System.ComponentModel;

namespace LSCore.SortAndPage.Contracts;

public class LSCoreSortableRequest<TSortColumn>
	where TSortColumn : struct
{
	public TSortColumn? SortColumn { get; set; }
	public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
}
