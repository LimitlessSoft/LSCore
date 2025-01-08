namespace LSCore.Contracts.Configurations;

public sealed class LSCoreAuthorizationConfiguration
{
    public string SecurityKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}