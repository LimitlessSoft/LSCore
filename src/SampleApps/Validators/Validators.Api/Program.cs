using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLSCoreDependencyInjection("Validators");

builder.Services.AddControllers();
var app = builder.Build();

app.UseLSCoreHandleException();

app.UseLSCoreDependencyInjection();

app.MapControllers();

app.Run();