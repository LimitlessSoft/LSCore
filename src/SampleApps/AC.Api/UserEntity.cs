using LSCore.Contracts.Interfaces;

namespace AC.Api;

public class UserEntity : ILSCoreAuthorizable
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
}