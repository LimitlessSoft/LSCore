using LSCore.Auth.Role.Contracts;

namespace LSCore.Auth.Role.Domain;

public class LSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>(
	ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole> userRepository
) : ILSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>
	where TRole : Enum
{
	public bool IsInRole(TAuthEntityIdentifier identifier, params TRole[] role)
	{
		var user = userRepository.GetOrDefault(identifier);
		return user != null && role.Contains(user.Role);
	}
}
