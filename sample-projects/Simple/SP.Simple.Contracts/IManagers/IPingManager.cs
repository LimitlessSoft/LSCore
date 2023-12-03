using LSCore.Contracts.Http;

namespace SP.Simple.Contracts.IManagers
{
    public interface IPingManager
    {
        LSCoreResponse GetPing();
    }
}
