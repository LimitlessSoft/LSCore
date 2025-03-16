using LSCore.Auth.Permission.DependencyInjection;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;
using Sample.AuthPermission.Api.Enums;
using Sample.AuthPermission.Api.Interfaces;
using Sample.AuthPermission.Api.Managers;
using Sample.AuthPermission.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.AddLSCoreAuthUserPass<string, AuthManager, UserRepository>(
	new LSCoreAuthUserPassConfiguration()
	{
		SecurityKey = "s21rv211r1dcd21e2v1vre1d1vd21vd12evd212dv1",
		Issuer = "Sample.AuthRole.Api",
		Audience = "Sample.AuthRole.Api"
	}
);
builder.AddLSCoreAuthPermission<string, UserPermission, UserRepository>();
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.UseLSCoreAuthUserPass<string>();
app.UseLSCoreAuthPermission<string, UserPermission>();
app.MapControllers();
app.Run();
