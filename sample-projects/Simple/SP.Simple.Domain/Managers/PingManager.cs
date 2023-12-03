using LSCore.Contracts.Http;
using SP.Simple.Contracts.IManagers;

namespace SP.Simple.Domain.Managers
{
    public class PingManager : IPingManager
    {
        /// <summary>
        /// Returns new empty response object with default values
        /// </summary>
        /// <returns></returns>
        public LSCoreResponse GetPing() =>
            new LSCoreResponse();
    }
}
