using LSCore.Auth.Contracts;

namespace LSCore.Auth.Permission.Contracts;

public class LSCoreAuthPermissionAttribute<TPermission> : LSCoreAuthAttribute
	where TPermission : Enum
{
	public bool RequireAll { get; } = true;
	public TPermission[] Permissions { get; }

	public LSCoreAuthPermissionAttribute(params TPermission[] permissions)
	{
		if (permissions == null || permissions.Length == 0)
			throw new ArgumentException(
				"At least one permission must be specified.",
				nameof(permissions)
			);

		Permissions = permissions;
	}

	public LSCoreAuthPermissionAttribute(bool requireAll, params TPermission[] permissions)
		: this(permissions)
	{
		RequireAll = requireAll;
	}
}
