using LSCore.Contracts.Exceptions;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Interfaces.Repositories;
using Sample.AuthorizationSimple.Api.Entities;

namespace Sample.AuthorizationSimple.Api.Repositories;

public class UserRepository : ILSCoreAuthorizableEntityRepository
{
    private static List<UserEntity> _users =
    [
        new() { Username = "user1", Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password") },
        new() { Username = "user2", Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password1") },
        new() { Username = "user3", Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password2") }
    ];
    
    public ILSCoreAuthorizable? Get(string username) // ILSCoreAuthorizableEntityRepository method implementation
    {
        return _users.FirstOrDefault(x => x.Username == username);
    }

    public void SetRefreshToken(long id, string refreshToken) // ILSCoreAuthorizableEntityRepository method implementation
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        if(user == null)
            throw new LSCoreNotFoundException();
        
        user.RefreshToken = refreshToken;
    }

    public ILSCoreAuthorizable? GetByRefreshToken(string refreshToken) // ILSCoreAuthorizableEntityRepository method implementation
    {
        return _users.FirstOrDefault(x => x.RefreshToken == refreshToken);
    }
}