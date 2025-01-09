using LSCore.Contracts.Entities;
using LSCore.Contracts.Interfaces;

namespace Sample.Authorization.Contracts.Entities;

public class UserEntity : LSCoreEntity, ILSCoreAuthorizable
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
}