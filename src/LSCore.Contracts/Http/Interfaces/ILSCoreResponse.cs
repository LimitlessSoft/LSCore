using System.Net;

namespace LSCore.Contracts.Http.Interfaces
{
    public interface ILSCoreResponse
    {
        HttpStatusCode Status { get; set; }
        bool NotOk { get; }
        List<string>? Errors { get; set; }
    }

    public interface ILSCoreResponse<TPayload> : ILSCoreResponse
    {
        TPayload? Payload { get; set; }
    }
}
