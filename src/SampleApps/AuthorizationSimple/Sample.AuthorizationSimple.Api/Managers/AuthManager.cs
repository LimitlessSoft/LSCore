using LSCore.Contracts.Configurations;
using LSCore.Contracts.Interfaces.Repositories;
using LSCore.Domain.Managers;

namespace Sample.AuthorizationSimple.Api.Managers;

public class AuthManager (LSCoreAuthorizationConfiguration authorizationConfiguration, ILSCoreAuthorizableEntityRepository authorizableEntityRepository)
    : LSCoreAuthorizeManager(authorizationConfiguration, authorizableEntityRepository);