using LSCore.Contracts;
using LSCore.Contracts.Configurations;
using LSCore.Domain.Managers;
using Sample.Authorization.Contracts.Dtos.Users;
using Sample.Authorization.Contracts.Interfaces.IManagers;
using Sample.Authorization.Contracts.Interfaces.Repositories;
using Sample.Authorization.Contracts.Requests.Users;

namespace Sample.Authorization.Domain.Managers;

public class UserManager (
    LSCoreContextUser contextUser,
    LSCoreAuthorizationConfiguration authorizationConfiguration,
    IUserRepository userRepository)
        : LSCoreAuthorizeManager(authorizationConfiguration, userRepository), IUserManager
{
    public void SetPassword(UsersSetPasswordRequest request) =>
        userRepository.SetPassword(request.Username, BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password));

    public UsersGetMeDto GetMe()
    {
        if (contextUser.Id == null)
            return new UsersGetMeDto();
        
        var user = userRepository.Get(contextUser.Id.Value);
        return new UsersGetMeDto
        {
            Username = user.Username
        };
    }
}