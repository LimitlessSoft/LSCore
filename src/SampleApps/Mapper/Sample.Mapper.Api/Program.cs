using LSCore.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.AddLSCoreDependencyInjection("Sample.Mapper");
var app = builder.Build();
app.UseLSCoreDependencyInjection();
app.UseLSCoreExceptionsHandler();

// app.UseLSCoreDependencyInjection();
app.MapControllers();
app.Run();
