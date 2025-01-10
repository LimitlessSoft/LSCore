namespace LSCore.Contracts.Interfaces.Repositories;

public interface ILSCoreAuthorizableEntityRepository
{
    ILSCoreAuthorizable Get(string username);
    void SetRefreshToken(long id, string refreshToken);
}