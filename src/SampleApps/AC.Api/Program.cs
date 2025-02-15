using AC.Api;
using LSCore.Contracts.Configurations;
using LSCore.Framework.Extensions;

InitializeUsers();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreAuthorization<AuthManager, UsersRepository>(new LSCoreAuthorizationConfiguration
{
    SecurityKey = "12r18f10cndj2190cdn120asgsagsagsagsagasga9dc10ed1",
    Issuer = "AC.Api",
    Audience = "AC.Api",
});
builder.AddLSCoreApiKeyAuthorization(new LSCoreApiKeyConfiguration()
{
    ApiKeys = ["develop"],
    BreakOnFailedAuthorization = false
});

var app = builder.Build();
app.UseLSCoreHandleException();
app.UseLSCoreApiKeyAuthorization();
app.UseLSCoreAuthorization();
app.MapControllers();
app.Run();

return;

static void InitializeUsers()
{
    UsersRepository._users.Add(new UserEntity
    {
        Id = 1,
        Username = "admin",
        Password = BCrypt.Net.BCrypt.EnhancedHashPassword("admin")
    });
    UsersRepository._users.Add(new UserEntity
    {
        Id = 2,
        Username = "user",
        Password = BCrypt.Net.BCrypt.EnhancedHashPassword("user")
    });
}