using LSCore.Contracts.Requests;
using Sample.Minimal.Contracts.Dtos.Users;

namespace Sample.Minimal.Contracts.IManagers;

public interface IUserManager
{
    List<UserDto> GetMultiple();
    UserDto GetSingular(LSCoreIdRequest request);
}