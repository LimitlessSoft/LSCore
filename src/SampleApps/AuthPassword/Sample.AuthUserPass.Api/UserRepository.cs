using LSCore.Auth.Contracts;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;
using LSCore.Exceptions;

namespace Sample.AuthUserPass.Api;

public class UserRepository : ILSCoreAuthUserPassIdentityEntityRepository<string>
{
	private static List<UserEntity> _users = [];

	public UserRepository()
	{
		_users.Add(
			new UserEntity()
			{
				Id = 1,
				Password = LSCoreAuthUserPassHelpers.HashPassword("SomePassword"),
				Username = "FirstUser"
			}
		);
		_users.Add(
			new UserEntity()
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

	public ILSCoreAuthUserPassEntity<string>? GetOrDefault(string identifier) =>
		_users.FirstOrDefault(x => x.Identifier == identifier);

	public void SetRefreshToken(string entityIdentifier, string refreshToken)
	{
		var user = _users.FirstOrDefault(x => x.Identifier == entityIdentifier);
		if (user == null)
			throw new LSCoreNotFoundException();
		user.RefreshToken = refreshToken;
	}
}
