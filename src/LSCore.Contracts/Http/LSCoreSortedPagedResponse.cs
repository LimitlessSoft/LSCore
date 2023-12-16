using LSCore.Contracts.Http;
using LSCore.Contracts.Interfaces;

namespace LSCore.Contracts.Responses
{
    public partial class LSCoreSortedPagedResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public PaginationData Pagination { get; set; } = new PaginationData();

        public LSCoreSortedPagedResponse()
        {

        }

        public LSCoreSortedPagedResponse(List<TEntity> payload, ILSCorePageable pageable, int totalElementsCount)
        {
            Payload = payload;
            Pagination.CurrentPage = pageable.CurrentPage;
            Pagination.PageSize = pageable.PageSize;
            Pagination.TotalElementsCount = totalElementsCount;
        }
    }
}
