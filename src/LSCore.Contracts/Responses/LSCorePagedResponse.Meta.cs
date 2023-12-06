using LSCore.Contracts.Http;

namespace LSCore.Contracts.Responses
{
    public partial class LSCorePagedResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public class Meta
        {
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public int TotalElementsCount { get; set; }
        }
    }
}
