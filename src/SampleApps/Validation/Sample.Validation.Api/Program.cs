using LSCore.DependencyInjection;
using LSCore.Exceptions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// This will automatically resolve all the validators along with other things
// Read more about AddLSCoreDependencyInjection in documentation
builder.AddLSCoreDependencyInjection("Sample.Validation");
var app = builder.Build();
app.UseLSCoreDependencyInjection();
app.UseLSCoreExceptionsHandler();
app.MapControllers();
app.Run();
