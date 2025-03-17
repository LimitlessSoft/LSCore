namespace LSCore.Auth.Permission.Contracts;

public interface ILSCoreAuthPermissionIdentityEntityRepository<TEntityIdentifier, TPermission>
	where TPermission : Enum
{
	ILSCoreAuthPermissionEntity<TEntityIdentifier, TPermission>? GetOrDefault(
		TEntityIdentifier identifier
	);
}
