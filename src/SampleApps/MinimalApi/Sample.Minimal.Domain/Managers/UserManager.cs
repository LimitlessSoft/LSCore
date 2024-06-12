using Sample.Minimal.Contracts.SortColumnCodes;
using Sample.Minimal.Contracts.Requests.Users;
using Sample.Minimal.Contracts.Dtos.Users;
using Sample.Minimal.Contracts.IManagers;
using Sample.Minimal.Contracts.Entities;
using Microsoft.Extensions.Logging;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.Responses;
using LSCore.Contracts.Requests;
using Sample.Minimal.Repository;
using LSCore.Domain.Extensions;
using LSCore.Domain.Managers;

namespace Sample.Minimal.Domain.Managers;

public class UserManager : LSCoreManagerBase<UserEntity>, IUserManager
{
    public UserManager(ILogger<UserEntity> logger)
        : base(logger)
    {
    }

    public UserManager(ILogger<UserEntity> logger, SampleDbContext dbContext)
        : base(logger, dbContext)
    {
    }

    public List<UserDto> GetMultiple() =>
        Queryable<UserEntity>()
            .Where(x => x.IsActive)
            .ToDtoList<UserEntity, UserDto>();

    public UserDto GetSingular(LSCoreIdRequest request) =>
        Queryable<UserEntity>()
            .FirstOrDefault(x => x.Id == request.Id)?
            .ToDto<UserEntity, UserDto>()
        ?? throw new LSCoreNotFoundException();

    public LSCoreSortedAndPagedResponse<UserDto> GetMultipleSortedAndPaged(LSCoreSortableAndPageableRequest request) =>
        Queryable<UserEntity>()
            .Where(x => x.IsActive)
            .ToSortedAndPagedResponse<UserEntity, UsersSortColumnCodes.Users, UserDto>(request, UsersSortColumnCodes.UsersSortRules);
}