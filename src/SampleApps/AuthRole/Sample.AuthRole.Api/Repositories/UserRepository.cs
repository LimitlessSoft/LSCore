using LSCore.Auth.Role.Contracts;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;
using LSCore.Exceptions;
using Sample.AuthRole.Api.Entities;
using Sample.AuthRole.Api.Enums;

namespace Sample.AuthRole.Api.Repositories;

public class UserRepository
	: ILSCoreAuthUserPassIdentityEntityRepository<string>,
		ILSCoreAuthRoleIdentityEntityRepository<string, UserRole>
{
	private static List<UserEntity> _users = [];

	public UserRepository()
	{
		_users.Add(
			new UserEntity
			{
				Id = 1,
				Password = LSCoreAuthUserPassHelpers.HashPassword("SomePassword"),
				Username = "FirstUser"
			}
		);
		_users.Add(
			new UserEntity
			{
				Id = 2,
				Role = UserRole.Administrator,
				Password = LSCoreAuthUserPassHelpers.HashPassword("SomePassword"),
				Username = "SecondUser"
			}
		);
		_users.Add(
			new UserEntity()
			{
				Id = 3,
				Password = LSCoreAuthUserPassHelpers.HashPassword("SomePassword"),
				Username = "ThirdUser"
			}
		);
	}

	ILSCoreAuthUserPassEntity<string>? ILSCoreAuthUserPassIdentityEntityRepository<string>.GetOrDefault(
		string identifier
	) => _users.FirstOrDefault(x => x.Identifier == identifier);

	public void SetRefreshToken(string entityIdentifier, string refreshToken)
	{
		var user = _users.FirstOrDefault(x => x.Identifier == entityIdentifier);
		if (user == null)
			throw new LSCoreNotFoundException();
		user.RefreshToken = refreshToken;
	}

	public ILSCoreAuthRoleEntity<string, UserRole>? GetOrDefault(string identifier) =>
		_users.FirstOrDefault(x => x.Identifier == identifier);
}
