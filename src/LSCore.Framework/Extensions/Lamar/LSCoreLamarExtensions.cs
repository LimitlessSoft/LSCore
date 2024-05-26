using LSCore.Contracts.Interfaces;
using Lamar.Scanning.Conventions;
using FluentValidation;

namespace LSCore.Framework.Extensions.Lamar;

public static class LSCoreLamarExtensions
{
    /// <summary>
    /// Implement LSCore services scanning
    /// </summary>
    /// <param name="services"></param>
    public static void LSCoreServicesLamarScan(this IAssemblyScanner services)
    {
        services.AssembliesAndExecutablesFromApplicationBaseDirectory((a) => a.GetName()!.Name!.StartsWith("LSCore"));
        
        services.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
        services.ConnectImplementationsToTypesClosing(typeof(ILSCoreFilter<>));
        services.ConnectImplementationsToTypesClosing(typeof(ILSCoreIncludes<>));
        services.ConnectImplementationsToTypesClosing(typeof(ILSCoreDtoMapper<,>));
    }
}