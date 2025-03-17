using LSCore.Auth.Permission.Contracts;

namespace LSCore.Auth.Permission.Domain;

public class LSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>(
	ILSCoreAuthPermissionIdentityEntityRepository<TAuthEntityIdentifier, TPermission> userRepository
) : ILSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>
	where TPermission : Enum
{
	public bool HasPermission(
		TAuthEntityIdentifier identifier,
		bool requireAll,
		params TPermission[] permissions
	)
	{
		var user = userRepository.GetOrDefault(identifier);
		if (user == null)
			return false;

		foreach (var permission in permissions)
		{
			var hasPermission = user.Permissions.Contains(permission);
			switch (requireAll)
			{
				case false when hasPermission:
					return true;
				case true when !hasPermission:
					return false;
			}
		}

		return requireAll;
	}
}
