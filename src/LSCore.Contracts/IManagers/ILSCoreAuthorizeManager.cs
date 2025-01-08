using LSCore.Contracts.Dtos;

namespace LSCore.Contracts.IManagers;

public interface ILSCoreAuthorizeManager
{
    LSCoreJwtDto Authorize(string username, string password);
}