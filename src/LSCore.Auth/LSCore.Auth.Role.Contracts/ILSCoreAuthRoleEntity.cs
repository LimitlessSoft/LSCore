using LSCore.Auth.Contracts;

namespace LSCore.Auth.Role.Contracts;

public interface ILSCoreAuthRoleEntity<out TEntityIdentifier, TRole>
	: ILSCoreAuthEntity<TEntityIdentifier>
	where TRole : Enum
{
	TRole Role { get; set; }
}
