using LSCore.Auth.Contracts;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;

namespace Sample.AuthCombined.Api;

public class AuthManager(
	ILSCoreAuthUserPassIdentityEntityRepository<string> userPassIdentityEntityRepository,
	LSCoreAuthUserPassConfiguration configuration
) : LSCoreAuthUserPassManager<string>(userPassIdentityEntityRepository, configuration) { }
