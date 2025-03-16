using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;

namespace Sample.AuthRole.Api.Managers;

public class AuthManager(
	ILSCoreAuthUserPassIdentityEntityRepository<string> userPassIdentityEntityRepository,
	LSCoreAuthUserPassConfiguration configuration
) : LSCoreAuthUserPassManager<string>(userPassIdentityEntityRepository, configuration) { }
