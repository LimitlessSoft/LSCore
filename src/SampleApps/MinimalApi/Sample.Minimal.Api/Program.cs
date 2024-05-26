using Lamar;
using Lamar.Microsoft.DependencyInjection;
using LSCore.Domain;
using Sample.Minimal.Contracts.Constants;
using LSCore.Framework.Extensions.Lamar;
using LSCore.Framework.Middlewares;
using LSCore.Framework.Extensions;
using Sample.Minimal.Repository;

// Creating application builder
var builder = WebApplication.CreateBuilder(args);

// Load configuration from json file and environment variables
builder.Configuration
    .AddJsonFile(Constants.General.AppSettings, optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Using lamar as DI container
builder.Host.UseLamar((_, registry) =>
{
    // All services registration should go here
    
    // Register configuration root
    builder.Services.AddSingleton<IConfigurationRoot>(builder.Configuration);
    
    // Register services
    registry.Scan(x =>
    {
        x.TheCallingAssembly();
        x.AssembliesAndExecutablesFromApplicationBaseDirectory((a) => a.GetName()!.Name!.StartsWith("Sample.Minimal"));
        
        x.WithDefaultConventions();
        x.LSCoreServicesLamarScan();
    });
    
    // Register database
    registry.RegisterDatabase(builder.Configuration);

    registry.AddControllers();
    registry.AddEndpointsApiExplorer();
    registry.AddSwaggerGen();
});

// Add dotnet logging
builder.LSCoreAddLogging();

var app = builder.Build();

LSCoreDomainConstants.Container = app.Services.GetService<IContainer>();

// Add exception handling middleware
// It is used to handle exceptions globally
app.UseMiddleware<LSCoreHandleExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();