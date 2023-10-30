using LSCore.Contracts.Http.Interfaces;
using System.Net;

namespace LSCore.Contracts.Http
{
    public class LSCoreResponse : ILSCoreResponse
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public bool NotOk => Convert.ToInt16(Status).ToString()[0] != '2';
        public List<string>? Errors { get; set; } = null;

        public static LSCoreResponse NotImplemented()
        {
            return new LSCoreResponse()
            {
                Status = HttpStatusCode.NotImplemented
            };
        }
        public static LSCoreResponse BadRequest()
        {
            return BadRequest(null);
        }
        public static LSCoreResponse BadRequest(params string[]? errorMessages)
        {
            return new LSCoreResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Errors = errorMessages == null ? null : new List<string>(errorMessages)
            };
        }
        public static LSCoreResponse InternalServerError()
        {
            return new LSCoreResponse()
            {
                Status = HttpStatusCode.InternalServerError
            };
        }
        public static LSCoreResponse NoContent()
        {
            return new LSCoreResponse()
            {
                Status = HttpStatusCode.NoContent
            };
        }
        public static LSCoreResponse NotFound()
        {
            return new LSCoreResponse()
            {
                Status = HttpStatusCode.NotFound
            };
        }
    }

    public class LSCoreResponse<TPayload> : ILSCoreResponse<TPayload>
    {
        public LSCoreResponse()
        {

        }

        public LSCoreResponse(TPayload payload)
        {
            Payload = payload;
        }
        public TPayload? Payload { get; set; }
        public bool NotOk => Convert.ToInt16(Status).ToString()[0] != '2';
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public List<string>? Errors { get; set; } = null;

        public static LSCoreResponse<TPayload> NotImplemented()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.NotImplemented
            };
        }
        public static LSCoreResponse<TPayload> BadRequest()
        {
            return BadRequest(null);
        }
        public static LSCoreResponse<TPayload> BadRequest(params string[]? errorMessages)
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.BadRequest,
                Errors = errorMessages == null ? null : new List<string>(errorMessages)
            };
        }
        public static LSCoreResponse<TPayload> InternalServerError()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.InternalServerError
            };
        }
        public static LSCoreResponse<TPayload> NoContent()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.NoContent
            };
        }
        public static LSCoreResponse<TPayload> Forbidden()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.Forbidden
            };
        }
        public static LSCoreResponse<TPayload> Unauthorized()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.Unauthorized
            };
        }
        public static LSCoreResponse<TPayload> NotFound()
        {
            return new LSCoreResponse<TPayload>()
            {
                Status = HttpStatusCode.NotFound
            };
        }
    }
}
