using LSCore.Contracts.Interfaces.Repositories;
using Sample.Authorization.Contracts.Entities;

namespace Sample.Authorization.Contracts.Interfaces.Repositories;

public interface IUserRepository : ILSCoreAuthorizableEntityRepository
{
    UserEntity Get(long id);
    UserEntity? GetOrDefault(string username);
    void SetPassword(string username, string password);
}