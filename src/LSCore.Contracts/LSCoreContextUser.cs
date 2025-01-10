namespace LSCore.Contracts;

/// <summary>
/// Used to store the current user in the context.
/// Default behavior is to have scoped lifetime.
/// If use is authenticated, this object's Id will be set, otherwise it will be null.
/// </summary>
public class LSCoreContextUser
{
    public long? Id { get; set; }
}