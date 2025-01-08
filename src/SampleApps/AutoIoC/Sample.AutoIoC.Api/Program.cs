using LSCore.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLSCoreDependencyInjection("Sample.AutoIoC");

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();