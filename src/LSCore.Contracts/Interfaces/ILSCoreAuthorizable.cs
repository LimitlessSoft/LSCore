namespace LSCore.Contracts.Interfaces;

public interface ILSCoreAuthorizable
{
    public long Id { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
}