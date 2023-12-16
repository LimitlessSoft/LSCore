using LSCore.Contracts.Dtos;
using LSCore.Contracts.Http.Interfaces;
using System.Net;
namespace LSCore.Contracts.Http
{
    public class LSCoreFileResponse : ILSCoreResponse
    {
        public LSCoreFileDto? Payload { get; set; }
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        public bool NotOk => Convert.ToInt16(Status).ToString()[0] != '2';
        public List<string>? Errors { get; set; } = null;

        public static LSCoreFileResponse NotImplemented()
        {
            return new LSCoreFileResponse()
            {
                Status = HttpStatusCode.NotImplemented
            };
        }

        public static LSCoreFileResponse BadRequest()
        {
            return BadRequest(null);
        }
        public static LSCoreFileResponse BadRequest(params string[]? errorMessages)
        {
            return new LSCoreFileResponse()
            {
                Status = HttpStatusCode.BadRequest,
                Errors = errorMessages == null ? null : new List<string>(errorMessages)
            };
        }
        public static LSCoreFileResponse InternalServerError()
        {
            return new LSCoreFileResponse()
            {
                Status = HttpStatusCode.InternalServerError
            };
        }
        public static LSCoreFileResponse NoContent()
        {
            return new LSCoreFileResponse()
            {
                Status = HttpStatusCode.NoContent
            };
        }
        public static LSCoreFileResponse NotFound()
        {
            return new LSCoreFileResponse()
            {
                Status = HttpStatusCode.NotFound
            };
        }
    }
}
