using LSCore.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// First parameters tells to scan all referenced assemblies which starts with LayerInjection
// With this, LayerInjection.Contracts and LayerInjection.Domain will be scanned
// You will usually use this method in your API projects since most the time actual
// implementation of interfaces will be in the same ecosystem, however this case also shows how to use
// it if you have assembly with different name
// Problem comes with SCL.Domain which is not scanned because it doesn't start with "LayerInjection"
// and because of it, IAnotherService is not resolved
builder.AddLSCoreDependencyInjection("LayerInjection");
throw new Exception("Read explanation and comment out line above along with current line, then run application");

// To fix this, we will overwrite scanning predicate and  add SCL.Domain to the list of assemblies to scan
builder.AddLSCoreDependencyInjection("LayerInjection",
    (o) =>
{
    // This will overwrite the default predicate which scans only assemblies starting with LayerInjection
    o.Scan.SetShouldScanAssemblyPredicate((a) =>
    {
        var shouldIncludeThisAssembly = false;
        
        // Since default predicate is overwritten, we need to include LayerInjection ones too
        if (a.GetName()?.Name?.StartsWith("LayerInjection") == true)
            shouldIncludeThisAssembly = true;
        else if (a.GetName()?.Name?.StartsWith("SCL") == true) // With this one, we add SCL.Domain to the list of assemblies to scan for types
            shouldIncludeThisAssembly = true;

        return shouldIncludeThisAssembly;
    });
});
var app = builder.Build();

app.MapControllers();
app.Run();