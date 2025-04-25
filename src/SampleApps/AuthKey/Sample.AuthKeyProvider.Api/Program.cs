using LSCore.Auth.Key.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;
using Sample.AuthKeyProvider.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthKey<MyKeyProvider>(); // Will be registered as a scoped service
var app = builder.Build();
app.UseLSCoreExceptionsHandler();
app.UseLSCoreAuthKey();
app.MapControllers();
app.Run();
