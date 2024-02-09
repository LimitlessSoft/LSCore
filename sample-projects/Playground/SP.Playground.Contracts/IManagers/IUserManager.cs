using SP.Playground.Contracts.Dtos.Users;
using LSCore.Contracts.Responses;
using LSCore.Contracts.Http;
using SP.Playground.Contracts.Requests.Users;

namespace SP.Playground.Contracts.IManagers
{
    public interface IUserManager
    {
        LSCoreListResponse<UsersGetDto> GetMultiple();
        LSCoreSortedPagedResponse<UsersGetDto> GetMultipleSortedAndPaged(UsersGetMultipleSortedAndPagedRequest request);
    }
}
