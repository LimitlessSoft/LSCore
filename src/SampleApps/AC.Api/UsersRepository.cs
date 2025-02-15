using LSCore.Contracts.Exceptions;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.Interfaces.Repositories;

namespace AC.Api;

public class UsersRepository : ILSCoreAuthorizableEntityRepository
{
    public static List<UserEntity> _users = [];
    public ILSCoreAuthorizable? Get(string username) =>
        _users.FirstOrDefault(x => x.Username == username);

    public void SetRefreshToken(long id, string refreshToken)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        if (user == null)
            throw new LSCoreNotFoundException();
        user.RefreshToken = refreshToken;
    }

    public ILSCoreAuthorizable? GetByRefreshToken(string refreshToken) =>
        _users.FirstOrDefault(x => x.RefreshToken == refreshToken);
}