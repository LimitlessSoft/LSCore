namespace LSCore.Contracts.Interfaces.Repositories;

public interface ILSCoreAuthorizableEntityRepository
{
    ILSCoreAuthorizable Get<T>(T identifier);
    void SetRefreshToken<T>(T identifier, string refreshToken);
}