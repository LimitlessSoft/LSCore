using LSCore.Auth.Contracts;
using LSCore.Auth.Role.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Sample.AuthRole.Api.Enums;

namespace Sample.AuthRole.Api.Entities;

public class UserEntity : ILSCoreAuthUserPassEntity<string>, ILSCoreAuthRoleEntity<string, UserRole>
{
	public long Id { get; set; }
	public UserRole Role { get; set; } = UserRole.User;
	public string Username { get; set; }
	public string Identifier => Username;
	public string? RefreshToken { get; set; }
	public string Password { get; set; }
}
