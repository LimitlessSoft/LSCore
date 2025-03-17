namespace LSCore.Auth.Permission.Contracts;

public interface ILSCoreAuthPermissionManager<in TAuthEntityIdentifier, in TPermission>
	where TPermission : Enum
{
	bool HasPermission(
		TAuthEntityIdentifier identifier,
		bool requireAll,
		params TPermission[] permissions
	);
}
