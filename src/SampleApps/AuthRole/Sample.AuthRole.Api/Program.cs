using LSCore.Auth.Role.DependencyInjection;
using LSCore.Auth.Role.Domain;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;
using Sample.AuthRole.Api.Enums;
using Sample.AuthRole.Api.Managers;
using Sample.AuthRole.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthUserPass<string, AuthManager, UserRepository>(
	new LSCoreAuthUserPassConfiguration
	{
		SecurityKey = "s21rv211r1dcd21e2v1vre1d1vd21vd12evd212dv1",
		Issuer = "Sample.AuthRole.Api",
		Audience = "Sample.AuthRole.Api"
	}
);
builder.AddLSCoreAuthRole<string, UserRole, UserRepository>();
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.UseLSCoreAuthUserPass<string>();
app.UseLSCoreAuthRole<string, UserRole>();
app.MapControllers();
app.Run();
