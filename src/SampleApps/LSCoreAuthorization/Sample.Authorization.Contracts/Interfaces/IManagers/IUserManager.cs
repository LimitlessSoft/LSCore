using LSCore.Contracts.IManagers;
using Sample.Authorization.Contracts.Dtos.Users;
using Sample.Authorization.Contracts.Enums;
using Sample.Authorization.Contracts.Requests.Users;

namespace Sample.Authorization.Contracts.Interfaces.IManagers;

public interface IUserManager : ILSCoreAuthorizeManager, ILSCoreHasPermissionManager<Permission>, ILSCoreHasRoleManager<Role>
{
    void SetPassword(UsersSetPasswordRequest request);
    UsersGetMeDto GetMe();
}