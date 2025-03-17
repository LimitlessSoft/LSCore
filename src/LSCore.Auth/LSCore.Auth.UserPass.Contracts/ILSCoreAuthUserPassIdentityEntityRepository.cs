namespace LSCore.Auth.UserPass.Contracts;

public interface ILSCoreAuthUserPassIdentityEntityRepository<TEntityIdentifier>
{
	ILSCoreAuthUserPassEntity<TEntityIdentifier>? GetOrDefault(TEntityIdentifier identifier);
	void SetRefreshToken(TEntityIdentifier entityIdentifier, string refreshToken);
}
