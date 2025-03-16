using LSCore.Auth.Permission.Contracts;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;
using LSCore.Exceptions;
using Sample.AuthPermission.Api.Entities;
using Sample.AuthPermission.Api.Enums;
using Sample.AuthPermission.Api.Interfaces;

namespace Sample.AuthPermission.Api.Repositories;

public class UserRepository
	: IUserRepository,
		ILSCoreAuthUserPassIdentityEntityRepository<string>,
		ILSCoreAuthPermissionIdentityEntityRepository<string, UserPermission>
{
	private static List<UserEntity> _users = [];

	public UserRepository()
	{
		_users.Add(
			new UserEntity
			{
				Id = 1,
				Password = LSCoreAuthUserPassHelpers.HashPassword("SomePassword"),
				Username = "FirstUser",
				Permissions = [UserPermission.Permission_One, UserPermission.Permission_Two]
			}
		);
		_users.Add(
			new UserEntity
			{
				Id = 2,
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

	UserEntity? IUserRepository.GetOrDefault(string username) =>
		_users.FirstOrDefault(x => x.Identifier == username);

	ILSCoreAuthPermissionEntity<
		string,
		UserPermission
	>? ILSCoreAuthPermissionIdentityEntityRepository<string, UserPermission>.GetOrDefault(
		string identifier
	) => _users.FirstOrDefault(x => x.Identifier == identifier);
}
