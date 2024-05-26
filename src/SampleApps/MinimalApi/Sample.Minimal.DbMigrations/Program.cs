using Sample.Minimal.Contracts.Constants;
using Sample.Minimal.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile(Constants.General.AppSettings, optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddSingleton<IConfigurationRoot>(builder.Configuration);
builder.Services.RegisterDatabase(builder.Configuration);
var app = builder.Build();
app.Run();
