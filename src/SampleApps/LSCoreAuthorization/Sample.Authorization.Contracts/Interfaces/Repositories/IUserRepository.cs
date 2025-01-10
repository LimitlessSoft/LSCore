using LSCore.Contracts.Interfaces.Repositories;
using Sample.Authorization.Contracts.Entities;
using Sample.Authorization.Contracts.Enums;

namespace Sample.Authorization.Contracts.Interfaces.Repositories;

public interface IUserRepository : ILSCoreAuthorizableEntityRepository
{
    UserEntity Get(long id);
    UserEntity? GetOrDefault(string username);
    UserEntity? GetOrDefault(long id);
    void SetPassword(string username, string password);
    HashSet<Permission> GetPermissions(long id);
}