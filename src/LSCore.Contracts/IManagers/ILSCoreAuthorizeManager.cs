using LSCore.Contracts.Dtos;

namespace LSCore.Contracts.IManagers;

public interface ILSCoreAuthorizeManager
{
    LSCoreJwtDto Authorize<T>(T identifier, string password);
}