using LSCore.Contracts.IManagers;

namespace Sample.Authorization.Contracts.Interfaces.IManagers;

public interface IUserManager : ILSCoreAuthorizeManager
{
    void SetPassword(string password);
}