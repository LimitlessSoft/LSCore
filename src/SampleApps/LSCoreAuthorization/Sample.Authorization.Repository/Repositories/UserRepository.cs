using LSCore.Contracts.Exceptions;
using LSCore.Contracts.Interfaces;
using Sample.Authorization.Contracts.Entities;
using Sample.Authorization.Contracts.Enums;
using Sample.Authorization.Contracts.Interfaces.Repositories;

namespace Sample.Authorization.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private static UserEntity[] _users =
    [
        new()
        {
            Id = 1,
            Username = "admin",
            Password = "password",
            IsActive = true,
            Permissions = [Permission.Access],
            Roles = [Role.User]
        }
    ];
    
    public ILSCoreAuthorizable Get(string username)
    {
        var entity = GetOrDefault(username);
        if (entity == null)
            throw new LSCoreNotFoundException();
        
        return entity;
    }

    public UserEntity Get(long id) => _users.First(x => x.IsActive && x.Id == id);

    public void SetRefreshToken(long id, string refreshToken)
    {
        var entity = Get(id);
        entity.RefreshToken = refreshToken;
    }

    public ILSCoreAuthorizable? GetByRefreshToken(string refreshToken)
    {
        return _users.FirstOrDefault(x => x.IsActive && x.RefreshToken == refreshToken);
    }

    public UserEntity? GetOrDefault(string username) => _users.FirstOrDefault(x => x.IsActive && x.Username == username);
    public UserEntity? GetOrDefault(long id) =>
        _users.FirstOrDefault(x => x.IsActive && x.Id == id);

    public void SetPassword(string username, string password)
    {
        var entity = Get(username);
        entity.Password = password;
    }

    public HashSet<Permission> GetPermissions(long id) =>
        Get(id).Permissions.ToHashSet();
}