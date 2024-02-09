using SP.Playground.Contracts.Enums.SortColumnCodes;
using SP.Playground.Repository.Includes.Users;
using SP.Playground.Contracts.Requests.Users;
using SP.Playground.Repository.Filters.Users;
using SP.Playground.Contracts.Dtos.Users;
using SP.Playground.Contracts.IManagers;
using SP.Playground.Contracts.Entities;
using Microsoft.Extensions.Logging;
using LSCore.Contracts.Extensions;
using LSCore.Contracts.Responses;
using SP.Playground.Repository;
using LSCore.Domain.Managers;
using LSCore.Contracts.Http;

namespace SP.Playground.Domain.Managers
{
    public class UserManager : LSCoreBaseManager<UserManager, UserEntity>, IUserManager
    {
        public UserManager(ILogger<UserManager> logger, SPPlaygroundDbContext dbContext)
            : base(logger, dbContext)
        {
        }

        public LSCoreListResponse<UsersGetDto> GetMultiple() =>
            Queryable()
            .LSCoreFilters<UserEntity, UsersGetMultipleFilter>()
            .LSCoreIncludes<UserEntity, UsersGetMultipleIncludes>()
            .ToLSCoreListResponse<UsersGetDto, UserEntity>();

        public LSCoreSortedPagedResponse<UsersGetDto> GetMultipleSortedAndPaged(UsersGetMultipleSortedAndPagedRequest request) =>
            Queryable()
            .LSCoreFilters<UserEntity, UsersGetMultipleFilter>()
            .LSCoreIncludes<UserEntity, UsersGetMultipleIncludes>()
            .ToLSCoreSortedPagedResponse<UsersGetDto, UserEntity, UsersSortColumnCodes.Users>(request, UsersSortColumnCodes.UsersSortRules);
    }
}
