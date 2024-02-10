# LSCore
### Free and open-source .NET Api framework

A .NET libraries which makes building your API faster and easier.

Check out sample-projects to see detailed implemetations

# Contribution
To contribute fork this project, branch from `develop`, make change and make PR onto `develop` branch
Since there is no established protocol, your change may not be accepted.
Best thing to do is create issue and discuss change there until someone create protocol for the contributiors.
Code merged into `main` is automatically packed and pushed to nuget

![GitHub last commit (branch)](https://img.shields.io/github/last-commit/LimitlessSoft/LSCore/develop?label=Last%20develop%20commit)
![GitHub Release](https://img.shields.io/github/v/release/LimitlessSoft/LSCore)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Contracts?label=LSCore.Contracts%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Contracts?label=LSCore.Contracts%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Domain?label=LSCore.Domain%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Domain?label=LSCore.Domain%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Framework?label=LSCore.Framework%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Framework?label=LSCore.Framework%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Repository?label=LSCore.Repository%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Repository?label=LSCore.Repository%20nuget)

# Feature Examples

## Entity Manager implementation
```
namespace Project.Contracts.IManagers
{
    public interface IUserManager
    {
        LSCoreListResponse<UserEntity> GetMultiple();
    }
}

namespace Project.Domain.Managers
{
    public class UserManager : LSCoreBaseManager<UserManager, UserEntity> : IUserManager
    {
        public UserManager(ILogger<UserManager> logger, ProjectDbContext dbContext)
            : base(logger, dbContext)
            {

            }

        public LSCoreListResponse<UserEntity> GetMultiple() =>
            Queryable()
            .ToLSCoreListResponse();
    }
}
```

## Querying inside Entity Manager
```
// Return response as list of entities
public LSCoreListResponse<UserEntity> GetMultiple() =>
    Queryable()
    .ToLSCoreListResponse();

// Return response as Sorted And Paged list of entities
// UsersGetMultipleRequest is request class which inherits : LSCoreSortablePageableRequest<UsersSortColumnCodes.Users>
public LSCoreListResponse<UserEntity> GetMultiple(UsersGetMultipleRequest request) =>
    Queryable()
    .ToLSCoreSortedPagedResponse<UserEntity, UsersSortColumnCodes.Users>(request, UsersSortColumnCodes.UsersSortRules);

// Filter query
public LSCoreListResponse<UserEntity> GetMultiple(UsersGetMultipleRequest request) =>
    Queryable()
    .LSCoreFilter(x => x.IsActive == true || x.Type == request.Type)
    .ToLSCoreListResponse();

// Include (left join) on query
public LSCoreListResponse<UserEntity> GetMultiple(UsersGetMultipleRequest request) =>
    Queryable()
    .LSCoreIncludes(x => x.City)
    .ToLSCoreListResponse();

// Then Include on query
public LSCoreListResponse<UserEntity> GetMultiple(UsersGetMultipleRequest request)
{
    var response = new LSCoreListResponse<UserEntity>();

    var qResponse = Queryable();
    response.merge(qResponse);
    if(response.NotOk)
        return response;

    var query = qResponse.Payload!;

    var users = query
        .Include(x => x.City)
        .ThenInclude(x => x.Street);

    response.Payload = users.ToList();
    return response;
}

```