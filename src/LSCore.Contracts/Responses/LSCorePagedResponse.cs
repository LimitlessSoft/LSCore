using LSCore.Contracts.Http;

namespace LSCore.Contracts.Responses
{
    public partial class LSCorePagedResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public LSCorePagedResponse()
        {

        }

        public LSCorePagedResponse(List<TEntity> payload)
        {
            Payload = payload;
        }
    }
}
