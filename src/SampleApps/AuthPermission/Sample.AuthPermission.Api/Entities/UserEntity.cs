using LSCore.Auth.Permission.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Sample.AuthPermission.Api.Enums;

namespace Sample.AuthPermission.Api.Entities;

public class UserEntity
	: ILSCoreAuthUserPassEntity<string>,
		ILSCoreAuthPermissionEntity<string, UserPermission>
{
	public long Id { get; set; }
	public ICollection<UserPermission> Permissions { get; set; } = [];
	public string Username { get; set; }
	public string Identifier => Username;
	public string? RefreshToken { get; set; }
	public string Password { get; set; }
}
