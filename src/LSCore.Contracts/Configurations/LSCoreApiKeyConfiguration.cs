namespace LSCore.Contracts.Configurations;

public class LSCoreApiKeyConfiguration
{
    /// <summary>
    /// Set of API keys that are allowed to access the API.
    /// </summary>
    public HashSet<string> ApiKeys { get; set; } = [];
}