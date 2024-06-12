using Sample.Minimal.Contracts.Requests.Users;
using Sample.Minimal.Contracts.Dtos.Users;
using LSCore.Contracts.Responses;
using LSCore.Contracts.Requests;

namespace Sample.Minimal.Contracts.IManagers;

public interface IUserManager
{
    List<UserDto> GetMultiple();
    UserDto GetSingular(LSCoreIdRequest request);
    LSCoreSortedAndPagedResponse<UserDto> GetMultipleSortedAndPaged(LSCoreSortableAndPageableRequest request);
}