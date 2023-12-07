using LSCore.Contracts.Interfaces;
using System.ComponentModel;

namespace LSCore.Contracts.Requests
{
    public class LSCoreSortablePageableRequest<TSortColumn> : ILSCorePageable
        where TSortColumn : struct
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public TSortColumn? SortColumn { get; set; }
        public ListSortDirection SortDirection { get; set; } = ListSortDirection.Ascending;
    }
}
