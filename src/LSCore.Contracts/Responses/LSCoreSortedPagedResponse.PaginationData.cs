using LSCore.Contracts.Http;

namespace LSCore.Contracts.Responses
{
    public partial class LSCoreSortedPagedResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public class PaginationData
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalElementsCount { get; set; }
        }
    }
}
