using SP.Simple.Contracts.Dtos.SortedPagedMock;
using System.Linq.Expressions;

namespace SP.Simple.Contracts.Enums.SortColumnCodes
{
    public static class SortedPagedMockSortColumnCodes
    {
        public enum SortedPagedMock
        {
            Name,
            Description,
        }

        public static Dictionary<SortedPagedMock, Expression<Func<GetSortedPagedMockDto, object>>> SortedPagedMockSortRules = new()
        {
            { SortedPagedMock.Name, x => x.Name },
            { SortedPagedMock.Description, x => x.Description },
        };
    }
}
