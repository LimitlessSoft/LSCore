using LSCore.Auth.Key.Contracts;
using LSCore.Auth.Key.DependencyInjection;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Auth.UserPass.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;
using Sample.AuthCombined.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthKey(
	new LSCoreAuthKeyConfiguration
	{
		ValidKeys = ["ThisIsFirstValidKey", "ThisIsSecondValidKey"],
		BreakOnFailedAuth = false
	}
);
builder.AddLSCoreAuthUserPass<string, AuthManager, UserRepository>(
	new LSCoreAuthUserPassConfiguration
	{
		SecurityKey = "2e1cn102dvu10br02u109rc012c21ubrv0231rc10rvn1u2",
		Audience = "Sample.AuthCombined.Api",
		Issuer = "Sample.AuthCombined.Api",
	}
);
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.MapControllers();
app.UseLSCoreAuthKey();
app.UseLSCoreAuthUserPass<string>();
app.Run();
