namespace LSCore.Contracts.Interfaces;

public interface ILSCoreAuthorizable
{
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
}