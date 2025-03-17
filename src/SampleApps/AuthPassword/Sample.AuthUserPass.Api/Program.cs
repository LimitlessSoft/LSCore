using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;
using Sample.AuthUserPass.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthUserPass<string, AuthManager, UserRepository>(
	new LSCoreAuthUserPassConfiguration
	{
		SecurityKey = "2e1cn102dvu10br02u109rc012c21ubrv0231rc10rvn1u2", // Should be loaded from secure place
		Issuer = "Sample.AuthUserPass.Api", // Should be loaded from secure place
		Audience = "Sample.AuthUserPass.Api", // Should be loaded from secure place
		TokenSkew = TimeSpan.FromSeconds(30) // Used for mock purpose to test refresh token. With default value, it will be 5 minutes
	}
);
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.UseLSCoreAuthUserPass<string>();
app.MapControllers();
app.Run();
