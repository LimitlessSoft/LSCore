namespace LSCore.ApiClient.Rest;

public interface ILSCoreApiClientRestConfiguration
{
    string BaseUrl { get; set; }
    string? LSCoreApiKey { get; set; }
}