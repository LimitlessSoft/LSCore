using Microsoft.AspNetCore.Http;
using LSCore.Contracts.Http.Interfaces;

namespace LSCore.Contracts.IManagers
{
    public interface ILSCoreBaseManager
    {
        void SetContext(HttpContext httpContext);
        bool IsContextInvalid(ILSCoreResponse response);
    }
}
