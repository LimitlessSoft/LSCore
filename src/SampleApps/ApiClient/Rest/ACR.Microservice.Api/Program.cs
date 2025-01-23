using LSCore.Contracts.Configurations;
using LSCore.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLSCoreApiKeyAuthorization(LoadApiKeyConfiguration());
builder.Services.AddControllers();
var app = builder.Build();
app.UseLSCoreHandleException();
app.UseLSCoreApiKeyAuthorization();
app.MapControllers();
app.Run();

return;

LSCoreApiKeyConfiguration LoadApiKeyConfiguration() =>
new ()
{
    ApiKeys = ["develop"] // Load this from secure place
};