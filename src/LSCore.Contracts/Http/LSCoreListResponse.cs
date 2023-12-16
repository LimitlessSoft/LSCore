using LSCore.Contracts.Http.Interfaces;
using System.Net;

namespace LSCore.Contracts.Http
{
    public class LSCoreListResponse<TEntity> : ILSCoreResponse<List<TEntity>>
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public bool NotOk => Convert.ToInt16(Status).ToString()[0] != '2';
        public List<TEntity> Payload { get; set; } = new List<TEntity>();
        public List<string>? Errors { get; set; } = null;

        public LSCoreListResponse()
        {

        }

        public LSCoreListResponse(List<TEntity> payload)
        {
            Payload = payload;
        }

        public static LSCoreListResponse<TEntity> NotImplemented()
        {
            return new LSCoreListResponse<TEntity>()
            {
                Status = HttpStatusCode.NotImplemented
            };
        }

        public static LSCoreListResponse<TEntity> BadRequest()
        {
            return BadRequest(null);
        }

        public static LSCoreListResponse<TEntity> BadRequest(params string[]? errorMessages)
        {
            return new LSCoreListResponse<TEntity>()
            {
                Status = HttpStatusCode.BadRequest,
                Errors = errorMessages == null ? null : new List<string>(errorMessages)
            };
        }
        public static LSCoreListResponse<TEntity> InternalServerError()
        {
            return new LSCoreListResponse<TEntity>()
            {
                Status = HttpStatusCode.InternalServerError
            };
        }
        public static LSCoreListResponse<TEntity> NoContent()
        {
            return new LSCoreListResponse<TEntity>()
            {
                Status = HttpStatusCode.NoContent
            };
        }
    }
}
