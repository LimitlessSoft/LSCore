namespace LSCore.ApiClient.Rest;

public class LSCoreApiClientRestConfiguration<TClient> : ILSCoreApiClientRestConfiguration
    where TClient : LSCoreApiClient
{
    public string BaseUrl { get; set; }
    public string? LSCoreApiKey { get; set; }
}