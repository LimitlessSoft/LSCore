using LSCore.Auth.Contracts;

namespace LSCore.Auth.Permission.Contracts;

public interface ILSCoreAuthPermissionEntity<out TEntityIdentifier, TPermission>
	: ILSCoreAuthEntity<TEntityIdentifier>
	where TPermission : Enum
{
	ICollection<TPermission> Permissions { get; set; }
}
