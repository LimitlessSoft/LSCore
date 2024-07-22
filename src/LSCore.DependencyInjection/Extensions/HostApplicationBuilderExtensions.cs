using Microsoft.Extensions.DependencyInjection;
using LSCore.DependencyInjection.Models;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace LSCore.DependencyInjection.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static void AddLSCoreDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.AddLSCoreDependencyInjection(null);
    }
    
    public static void AddLSCoreDependencyInjection(this IHostApplicationBuilder builder,
        Action<LSCoreDependencyInjectionOptions>? options)
    {
        options?.Invoke(new LSCoreDependencyInjectionOptions());
        
        #region Apply options
        if (Constants.IncludeCallingAssembly)
            Constants.Configuration.AssembliesToBeScanned.Add(Assembly.GetCallingAssembly());
        
        if (Constants.AssemblyAndExecutablesFromApplicationBaseDirectory != null)
            Constants.Configuration.AssembliesToBeScanned.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(Assembly.LoadFrom)
                .Where(Constants.AssemblyAndExecutablesFromApplicationBaseDirectory));
        #endregion
        
        builder.InitializeLSCoreDependencyInjection();
    }
    
    private static void InitializeLSCoreDependencyInjection(this IHostApplicationBuilder builder)
    {
        foreach (var type in Constants.Configuration.AssembliesToBeScanned.Distinct().SelectMany(a => a.GetTypes()))
        {
            if (Constants.WithDefaultConventions)
                builder.AddServicesFollowingDefaultConventions(type);
            
            if (Constants.IncludeLSCoreDtoMappers)
                builder.AddServicesFollowingLSCoreDtoMapperConventions(type);
            
            if(Constants.IncludeLSCoreValidators)
                builder.AddServicesFollowingLSCoreValidatorConventions(type);
        }
    }
    
    private static void AddServicesFollowingDefaultConventions(this IHostApplicationBuilder builder, Type? type)
    {
        if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType) return;
        
        var @interface = type.GetInterfaces().FirstOrDefault(x => x.Name == $"I{type.Name}");
        if (@interface == null) return;
        
        builder.Services.AddTransient(@interface, type);
    }

    private static void AddServicesFollowingLSCoreDtoMapperConventions(this IHostApplicationBuilder builder, Type? type)
    {
        if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType) return;
        
        var @interface = type.GetInterfaces().FirstOrDefault(x => x.Name.Contains("ILSCoreDtoMapper"));
        if (@interface == null) return;
        
        builder.Services.AddSingleton(@interface, type);
    }
    
    private static void AddServicesFollowingLSCoreValidatorConventions(this IHostApplicationBuilder builder, Type? type)
    {
        if (type is not { IsClass: true } || type.IsAbstract || type.IsGenericType) return;

        if (type.BaseType?.IsGenericType != true
            || type.BaseType.GetGenericTypeDefinition().Name.Contains("LSCoreValidatorBase")) return;
        
        builder.Services.AddTransient(type.BaseType, type);
    }
    
    public static void UseLSCoreDependencyInjection(this IHost host)
    {
        Container.ServiceProvider = host.Services.GetService<IServiceProvider>();
    }
}