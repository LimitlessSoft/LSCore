using LSCore.Auth.Contracts;

namespace LSCore.Auth.UserPass.Contracts;

public interface ILSCoreAuthPasswordManager<in TEntityIdentifier>
{
	LSCoreJwt Authenticate(TEntityIdentifier identifier, string password);
}
