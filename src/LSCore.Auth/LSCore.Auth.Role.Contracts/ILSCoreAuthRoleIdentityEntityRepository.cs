namespace LSCore.Auth.Role.Contracts;

public interface ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole>
	where TRole : Enum
{
	ILSCoreAuthRoleEntity<TAuthEntityIdentifier, TRole>? GetOrDefault(
		TAuthEntityIdentifier identifier
	);
}
