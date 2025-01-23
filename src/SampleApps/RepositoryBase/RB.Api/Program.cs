using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;
using RB.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<RBDbContext>();
builder.AddLSCoreDependencyInjection("RB");
var app = builder.Build();
app.UseLSCoreHandleException();
app.UseLSCoreDependencyInjection();
app.MapControllers();
app.Run();