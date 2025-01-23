using System.Net.Http.Json;
using ACR.UsersMicroservice.Dtos;
using ACR.UsersMicroservice.Requests;
using LSCore.ApiClient.Rest;

namespace ACR.UsersMicroservice.Client;

public class ACRUsersMicroserviceClient (LSCoreApiClientRestConfiguration<ACRUsersMicroserviceClient> configuration)
    : LSCoreApiClient(configuration)
{
    public async Task<List<UserDto>> GetMultipleAsync(GetMultipleRequest request)
    {
        var msResponse = await _httpClient.GetAsJsonAsync("/", request);
        HandleStatusCode(msResponse);
        return await msResponse.Content.ReadFromJsonAsync<List<UserDto>>();
    }
}