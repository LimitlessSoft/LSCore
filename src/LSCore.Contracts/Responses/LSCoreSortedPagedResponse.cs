using LSCore.Contracts.Http;

namespace LSCore.Contracts.Responses
{
    public partial class LSCoreSortedPagedResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public LSCoreSortedPagedResponse()
        {

        }

        public LSCoreSortedPagedResponse(List<TEntity> payload)
        {
            Payload = payload;
        }
    }
}
