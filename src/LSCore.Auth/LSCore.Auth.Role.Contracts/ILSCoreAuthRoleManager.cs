namespace LSCore.Auth.Role.Contracts;

public interface ILSCoreAuthRoleManager<in TAuthEntityIdentifier, in TRole>
	where TRole : Enum
{
	bool IsInRole(TAuthEntityIdentifier identifier, params TRole[] role);
}
