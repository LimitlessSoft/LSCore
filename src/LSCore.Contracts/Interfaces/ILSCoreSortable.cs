using System.ComponentModel;

namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreSortable<TSortColumn>
        where TSortColumn : struct
    {
        public TSortColumn? SortColumn { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
