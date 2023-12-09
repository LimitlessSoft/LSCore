using LSCore.Contracts.Http;
using LSCore.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Users;

namespace SP.Simple.Contracts.IManagers
{
    public interface IUserManager : ILSCoreBaseManager
    {
        LSCoreResponse<string> Login(LoginUserRequest request);
        LSCoreResponse<string> Me();
    }
}
