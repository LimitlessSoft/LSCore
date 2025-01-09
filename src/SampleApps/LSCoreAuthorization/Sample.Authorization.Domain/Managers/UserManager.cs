using LSCore.Contracts.Configurations;
using LSCore.Domain.Managers;
using Sample.Authorization.Contracts.Interfaces.IManagers;
using Sample.Authorization.Contracts.Interfaces.Repositories;

namespace Sample.Authorization.Domain.Managers;

public class UserManager (
    LSCoreAuthorizationConfiguration authorizationConfiguration,
    IUserRepository userRepository)
        : LSCoreAuthorizeManager(authorizationConfiguration, userRepository), IUserManager
{
    public void SetPassword(string password) =>
        userRepository.SetPassword(BCrypt.Net.BCrypt.EnhancedHashPassword(password));
}