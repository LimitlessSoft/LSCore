using ACR.Microservice.Client;
using ACR.UsersMicroservice.Client;
using LSCore.ApiClient.Rest;
using LSCore.ApiClient.Rest.DependencyInjection;
using LSCore.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreApiClientRest(LoadMicroservice1Configuration());
builder.AddLSCoreApiClientRest(LoadMicroservice2Configuration());
var app = builder.Build();
app.UseLSCoreHandleException();
app.MapControllers();
app.Run();

return;

LSCoreApiClientRestConfiguration<ACRMicroserviceClient> LoadMicroservice1Configuration()
{
    var configuration = new LSCoreApiClientRestConfiguration<ACRMicroserviceClient>
    {
        BaseUrl = "http://localhost:5230",
        LSCoreApiKey = "develop" // Load from some secure place
    };
    return configuration;
}
LSCoreApiClientRestConfiguration<ACRUsersMicroserviceClient> LoadMicroservice2Configuration()
{
    var configuration = new LSCoreApiClientRestConfiguration<ACRUsersMicroserviceClient>
    {
        BaseUrl = "http://localhost:5067",
        LSCoreApiKey = "develop" // Load from some secure place
    };
    return configuration;
}