using LSCore.Auth.Contracts;

namespace LSCore.Auth.UserPass.Contracts;

public interface ILSCoreAuthUserPassEntity<out TEntityIdentifier>
	: ILSCoreAuthEntity<TEntityIdentifier>
{
	public string? RefreshToken { get; set; }
	public string Password { get; set; }
}
