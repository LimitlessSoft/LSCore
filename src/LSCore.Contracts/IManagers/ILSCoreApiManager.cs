using LSCore.Contracts.Http;

namespace LSCore.Contracts.IManagers
{
    public interface ILSCoreApiManager
    {
        HttpClient HttpClient { get; set; }
        LSCoreResponse<TPayload> HandleResponse<TPayload>(HttpResponseMessage responseMessage);
        Task<LSCoreResponse<TPayload>> HandleResponseAsync<TPayload>(HttpResponseMessage responseMessage);
        LSCoreResponse HandleRawResponse(HttpResponseMessage responseMessage);
        LSCoreResponse<TPayload> HandleRawResponse<TPayload>(HttpResponseMessage responseMessage);
        Task<LSCoreResponse<TPayload>> HandleRawResponseAsync<TPayload>(HttpResponseMessage responseMessage);
        Task<LSCoreResponse<TPayload>> GetAsync<TPayload>(string endpoint);
        Task<LSCoreResponse<TPayload>> GetAsync<TRequest, TPayload>(string endpoint, TRequest request);
        Task<LSCoreResponse> GetRawAsync(string endpoint);
        Task<LSCoreResponse<TPayload>> GetRawAsync<TPayload>(string endpoint);
        Task<LSCoreResponse<TPayload>> GetRawAsync<TRequest, TPayload>(string endpoint, TRequest request);
        Task<LSCoreResponse> PostRawAsync(string endpoint);
        Task<LSCoreResponse> PutRawAsync(string endpoint);
        Task<LSCoreResponse<TPayload>> PutAsync<TPayload>(string endpoint);
        Task<LSCoreResponse<TPayload>> PostAsync<TPayload>(string endpoint);
        Task<LSCoreResponse<TPayload>> PutRawAsync<TPayload>(string endpoint);
        Task<LSCoreResponse<TPayload>> PostRawAsync<TPayload>(string endpoint);
        Task<LSCoreResponse> PostAsync<TRequest>(string endpoint, TRequest request);
        Task<LSCoreResponse> PutAsync<TRequest>(string endpoint, TRequest request);
        Task<LSCoreResponse<TPayload>> PostAsync<TRequest, TPayload>(string endpoint, TRequest request);
        Task<LSCoreResponse<TPayload>> PutAsync<TRequest, TPayload>(string endpoint, TRequest request);
        Task<LSCoreResponse> PostAsync<TRequest>(HttpClient httpClient, string endpoint, TRequest request);
        Task<LSCoreResponse> PutAsync<TRequest>(HttpClient httpClient, string endpoint, TRequest request);
        Task<LSCoreResponse<TPayload>> PostAsync<TRequest, TPayload>(HttpClient httpClient, string endpoint, TRequest request);
        Task<LSCoreResponse<TPayload>> PutAsync<TRequest, TPayload>(HttpClient httpClient, string endpoint, TRequest request);
        Task<LSCoreResponse> DeleteAsync(string endpoint);
    }
}
