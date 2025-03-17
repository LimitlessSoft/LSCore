using LSCore.Auth.Contracts;

namespace LSCore.Auth.UserPass.Contracts;

public interface ILSCoreAuthUserPassManager<in TEntityIdentifier>
{
	LSCoreJwt Authenticate(TEntityIdentifier identifier, string password);
}
