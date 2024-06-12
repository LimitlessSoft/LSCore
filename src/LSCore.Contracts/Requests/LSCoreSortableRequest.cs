using LSCore.Contracts.Interfaces;
using System.ComponentModel;

namespace LSCore.Contracts.Requests;

public class LSCoreSortableRequest<TSortColumn> : ILSCoreSortable<TSortColumn>
    where TSortColumn : struct
{
    public TSortColumn? SortColumn { get; set; }
    public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
}