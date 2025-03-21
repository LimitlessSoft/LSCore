using LSCore.Auth.Contracts;

namespace LSCore.Auth.UserPass.Contracts;

public class LSCoreAuthUserPassConfiguration : LSCoreAuthConfiguration
{
	public required string SecurityKey { get; set; }
	public required string Issuer { get; set; }
	public required string Audience { get; set; }
	public int AccessTokenExpirationMinutes { get; set; } = 30;
	public int RefreshTokenExpirationDays { get; set; } = 7;
	public TimeSpan TokenSkew { get; set; } = TimeSpan.FromMinutes(5);
}
