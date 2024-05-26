using LSCore.Contracts.Entities;

namespace Sample.Minimal.Contracts.Entities;

public class UserEntity : LSCoreEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
}