using LSCore.Contracts.Configurations;
using LSCore.Contracts.Interfaces.Repositories;
using LSCore.Domain.Managers;

namespace AC.Api;

public class AuthManager (
    LSCoreAuthorizationConfiguration authorizationConfiguration,
    ILSCoreAuthorizableEntityRepository authorizableEntityRepository)
    : LSCoreAuthorizeManager(authorizationConfiguration, authorizableEntityRepository);
