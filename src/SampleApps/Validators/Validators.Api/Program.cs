using LSCore.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLSCoreDependencyInjection("Validators");

builder.Services.AddControllers();
var app = builder.Build();

app.UseLSCoreDependencyInjection();

app.MapControllers();

app.Run();