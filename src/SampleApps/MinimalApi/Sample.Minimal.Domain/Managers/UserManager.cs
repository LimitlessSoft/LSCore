using LSCore.Contracts.Exceptions;
using LSCore.Contracts.Requests;
using LSCore.Domain.Extensions;
using Sample.Minimal.Contracts.IManagers;
using Sample.Minimal.Contracts.Entities;
using Microsoft.Extensions.Logging;
using Sample.Minimal.Repository;
using LSCore.Domain.Managers;
using Sample.Minimal.Contracts.Dtos.Users;

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
}