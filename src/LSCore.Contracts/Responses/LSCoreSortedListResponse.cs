using LSCore.Contracts.Http;

namespace LSCore.Contracts.Responses
{
    public class LSCoreSortedListResponse<TEntity> : LSCoreListResponse<TEntity>
    {
        public LSCoreSortedListResponse()
            : base()
        {

        }

        public LSCoreSortedListResponse(List<TEntity> payload)
            : base(payload)
        {

        }
    }
}
