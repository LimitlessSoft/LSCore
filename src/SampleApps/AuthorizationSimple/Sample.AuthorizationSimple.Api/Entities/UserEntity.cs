using LSCore.Contracts.Interfaces;

namespace Sample.AuthorizationSimple.Api.Entities;

public class UserEntity : ILSCoreAuthorizable
{
    public string Username { get; set; }
    public long Id { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
}