using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.Domain;
using Sample.AuthPermission.Api.Interfaces;

namespace Sample.AuthPermission.Api.Managers;

public class AuthManager(
	ILSCoreAuthUserPassIdentityEntityRepository<string> userPassIdentityEntityRepository,
	LSCoreAuthUserPassConfiguration configuration,
	IUserRepository userRepository
) : LSCoreAuthUserPassManager<string>(userPassIdentityEntityRepository, configuration) { }
