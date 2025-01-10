using LSCore.Contracts.Entities;
using LSCore.Contracts.Interfaces;
using Sample.Authorization.Contracts.Enums;

namespace Sample.Authorization.Contracts.Entities;

public class UserEntity : LSCoreEntity, ILSCoreAuthorizable
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    public List<Permission> Permissions { get; set; }
    public List<Role> Roles { get; set; }
}