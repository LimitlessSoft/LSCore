using LSCore.Auth.Contracts;

namespace LSCore.Auth.Role.Contracts;

public class LSCoreAuthRoleAttribute<TRole>(params TRole[] roles) : LSCoreAuthAttribute
	where TRole : Enum
{
	public TRole[] Roles { get; } = roles;
}
