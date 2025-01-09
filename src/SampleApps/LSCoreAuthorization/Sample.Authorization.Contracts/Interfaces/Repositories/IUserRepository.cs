using LSCore.Contracts.Interfaces.Repositories;

namespace Sample.Authorization.Contracts.Interfaces.Repositories;

public interface IUserRepository : ILSCoreAuthorizableEntityRepository
{
    void SetPassword(string password);
}