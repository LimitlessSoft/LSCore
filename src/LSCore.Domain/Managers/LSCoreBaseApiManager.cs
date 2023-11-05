using LSCore.Contracts.Extensions;
using LSCore.Contracts.Http;
using LSCore.Contracts.IManagers;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Json;

namespace LSCore.Domain.Managers
{
    public abstract class LSCoreBaseApiManager : ILSCoreApiManager
    {
        public HttpClient HttpClient { get; set; }

        public LSCoreBaseApiManager()
        {
            HttpClient = new HttpClient();
        }

        #region LSCoreResponse handlers
        public LSCoreResponse<TPayload> HandleResponse<TPayload>(HttpResponseMessage LSCoreResponseMessage)
        {
            if (LSCoreResponseMessage.NotOk())
                return LSCoreResponse<TPayload>.BadRequest();

            return JsonConvert.DeserializeObject<LSCoreResponse<TPayload>>(LSCoreResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
        public async Task<LSCoreResponse<TPayload>> HandleResponseAsync<TPayload>(HttpResponseMessage LSCoreResponseMessage)
        {
            if (LSCoreResponseMessage.NotOk())
                return LSCoreResponse<TPayload>.BadRequest();

            var LSCoreResponseString = await LSCoreResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LSCoreResponse<TPayload>>(LSCoreResponseString);
        }
        public LSCoreResponse HandleRawResponse(HttpResponseMessage LSCoreResponseMessage)
        {
            var LSCoreResponse = new LSCoreResponse();
            LSCoreResponse.Status = LSCoreResponseMessage.StatusCode;
            return LSCoreResponse;
        }
        public LSCoreResponse<TPayload> HandleRawResponse<TPayload>(HttpResponseMessage LSCoreResponseMessage)
        {
            var LSCoreResponse = new LSCoreResponse<TPayload>();
            LSCoreResponse.Status = LSCoreResponseMessage.StatusCode;
            LSCoreResponse.Payload = JsonConvert.DeserializeObject<TPayload>(LSCoreResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            return LSCoreResponse;
        }
        public async Task<LSCoreResponse<TPayload>> HandleRawResponseAsync<TPayload>(HttpResponseMessage LSCoreResponseMessage)
        {
            var LSCoreResponse = new LSCoreResponse<TPayload>();
            LSCoreResponse.Status = LSCoreResponseMessage.StatusCode;
            var converter = TypeDescriptor.GetConverter(typeof(TPayload));
            var content = await LSCoreResponseMessage.Content.ReadAsStringAsync();

            if (content[0] == '{' || content[0] == '[')
                LSCoreResponse.Payload = JsonConvert.DeserializeObject<TPayload>(await LSCoreResponseMessage.Content.ReadAsStringAsync());
            else
                LSCoreResponse.Payload = (TPayload)Convert.ChangeType(await LSCoreResponseMessage.Content.ReadAsStringAsync(), typeof(TPayload));
            return LSCoreResponse;
        }
        #endregion

        #region Get
        public async Task<LSCoreResponse<TPayload>> GetAsync<TPayload>(string endpoint)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.GetAsync(endpoint));
        }
        public async Task<LSCoreResponse<TPayload>> GetAsync<TRequest, TPayload>(string endpoint, TRequest request)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.GetAsJsonAsync<TRequest>(endpoint, request));
        }
        public async Task<LSCoreResponse> GetRawAsync(string endpoint)
        {
            return HandleRawResponse(await HttpClient.GetAsync(endpoint));
        }
        public async Task<LSCoreResponse<TPayload>> GetRawAsync<TPayload>(string endpoint)
        {
            return await HandleRawResponseAsync<TPayload>(await HttpClient.GetAsync(endpoint));
        }
        public async Task<LSCoreResponse<TPayload>> GetRawAsync<TRequest, TPayload>(string endpoint, TRequest request)
        {
            return await HandleRawResponseAsync<TPayload>(await HttpClient.GetAsJsonAsync<TRequest>(endpoint, request));
        }
        #endregion

        #region Post
        public async Task<LSCoreResponse> PostRawAsync(string endpoint)
        {
            return HandleRawResponse(await HttpClient.PostAsync(endpoint, null));
        }
        public async Task<LSCoreResponse<TPayload>> PostAsync<TPayload>(string endpoint)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.PostAsync(endpoint, null));
        }
        public async Task<LSCoreResponse<TPayload>> PostRawAsync<TPayload>(string endpoint)
        {
            return await HandleRawResponseAsync<TPayload>(await HttpClient.PostAsync(endpoint, null));
        }

        public async Task<LSCoreResponse> PostAsync<TRequest>(string endpoint, TRequest request)
        {
            return HandleRawResponse(await HttpClient.PostAsJsonAsync<TRequest>(endpoint, request));
        }
        public async Task<LSCoreResponse<TPayload>> PostAsync<TRequest, TPayload>(string endpoint, TRequest request)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.PostAsJsonAsync<TRequest>(endpoint, request));
        }

        public async Task<LSCoreResponse> PostAsync<TRequest>(HttpClient httpClient, string endpoint, TRequest request)
        {
            return HandleRawResponse(await httpClient.PostAsJsonAsync(endpoint, request));
        }
        public async Task<LSCoreResponse<TPayload>> PostAsync<TRequest, TPayload>(HttpClient httpClient, string endpoint, TRequest request)
        {
            return await HandleResponseAsync<TPayload>(await httpClient.PostAsJsonAsync(endpoint, request));
        }
        #endregion

        #region Put

        public async Task<LSCoreResponse> PutRawAsync(string endpoint)
        {
            return HandleRawResponse(await HttpClient.PutAsync(endpoint, null));
        }
        public async Task<LSCoreResponse<TPayload>> PutAsync<TPayload>(string endpoint)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.PutAsync(endpoint, null));
        }
        public async Task<LSCoreResponse<TPayload>> PutRawAsync<TPayload>(string endpoint)
        {
            return await HandleRawResponseAsync<TPayload>(await HttpClient.PutAsync(endpoint, null));
        }

        public async Task<LSCoreResponse> PutAsync<TRequest>(string endpoint, TRequest request)
        {
            return HandleRawResponse(await HttpClient.PutAsJsonAsync<TRequest>(endpoint, request));
        }
        public async Task<LSCoreResponse<TPayload>> PutAsync<TRequest, TPayload>(string endpoint, TRequest request)
        {
            return await HandleResponseAsync<TPayload>(await HttpClient.PutAsJsonAsync<TRequest>(endpoint, request));
        }

        public async Task<LSCoreResponse> PutAsync<TRequest>(HttpClient httpClient, string endpoint, TRequest request)
        {
            return HandleRawResponse(await httpClient.PutAsJsonAsync(endpoint, request));
        }
        public async Task<LSCoreResponse<TPayload>> PutAsync<TRequest, TPayload>(HttpClient httpClient, string endpoint, TRequest request)
        {
            return await HandleResponseAsync<TPayload>(await httpClient.PutAsJsonAsync(endpoint, request));
        }
        #endregion

        #region Delete
        public async Task<LSCoreResponse> DeleteAsync(string endpoint)
        {
            return HandleRawResponse(await HttpClient.DeleteAsync(endpoint));
        }
        #endregion
    }
}
