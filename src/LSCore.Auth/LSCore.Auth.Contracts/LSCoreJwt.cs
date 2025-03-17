namespace LSCore.Auth.Contracts;

public class LSCoreJwt
{
	public required string AccessToken { get; set; }
	public required string RefreshToken { get; set; }
}
