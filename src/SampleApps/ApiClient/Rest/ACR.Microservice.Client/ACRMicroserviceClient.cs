using System.Net.Http.Json;
using ACR.Microservice.Contracts.Dtos.Products;
using ACR.Microservice.Contracts.Requests.Products;
using LSCore.ApiClient.Rest;

namespace ACR.Microservice.Client;

public class ACRMicroserviceClient (LSCoreApiClientRestConfiguration<ACRMicroserviceClient> configuration)
    : LSCoreApiClient(configuration)
{
    public async Task<List<ProductDto>> GetMultipleAsync(GetMultipleRequest request)
    {
        var msResponse = await _httpClient.GetAsJsonAsync("/", request);
        HandleStatusCode(msResponse);
        return await msResponse.Content.ReadFromJsonAsync<List<ProductDto>>();
    }
}