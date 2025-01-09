namespace LSCore.Contracts.IManagers;

public interface ILSCoreHasRoleManager<in TPermissionEnum>
    where TPermissionEnum : Enum
{
    public bool HasRole(long userId, params TPermissionEnum[] permissions);
}