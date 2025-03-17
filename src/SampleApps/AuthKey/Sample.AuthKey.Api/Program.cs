using LSCore.Auth.Key.Contracts;
using LSCore.Auth.Key.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthKey(
	new LSCoreAuthKeyConfiguration { ValidKeys = ["ThisIsFirstValidKey", "ThisIsSecondValidKey"] }
);
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.UseLSCoreAuthKey();
app.MapControllers();
app.Run();
