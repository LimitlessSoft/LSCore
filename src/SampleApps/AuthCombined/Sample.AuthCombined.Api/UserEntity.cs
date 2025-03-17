using LSCore.Auth.UserPass.Contracts;

namespace Sample.AuthCombined.Api;

public class UserEntity : ILSCoreAuthUserPassEntity<string>
{
	public long Id { get; set; }
	public string Username { get; set; }
	public string Identifier => Username;
	public string? RefreshToken { get; set; }
	public string Password { get; set; }
}
