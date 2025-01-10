namespace LSCore.Contracts.IManagers;

public interface ILSCoreHasPermissionManager<in TPermissionEnum>
    where TPermissionEnum : Enum
{
    public bool HasPermission(long userId, params TPermissionEnum[] permissions);
}