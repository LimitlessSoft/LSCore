using LSCore.Contracts.Interfaces;
using Sample.Authorization.Contracts.Entities;
using Sample.Authorization.Contracts.Interfaces.Repositories;

namespace Sample.Authorization.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private static UserEntity _user = new UserEntity
    {
        Id = 1,
        Password = "password",
    };

    public ILSCoreAuthorizable Get<T>(T identifier) => _user;

    public void SetRefreshToken<T>(T identifier, string refreshToken) => _user.RefreshToken = refreshToken;
    public void SetPassword(string password) => _user.Password = password;
}