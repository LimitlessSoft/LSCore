using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;

namespace Sample.AuthUserPass.Api;

public class AuthManager(
	ILSCoreAuthUserPassIdentityEntityRepository<string> userPassIdentityEntityRepository,
	LSCoreAuthUserPassConfiguration configuration
) : LSCoreAuthUserPassManager<string>(userPassIdentityEntityRepository, configuration);
