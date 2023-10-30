namespace LSCore.Contracts.Http.Interfaces
{
    public interface ILSCoreListResponse<TPayload> : ILSCoreResponse
    {
        List<TPayload> Payload { get; set; }
    }
}
