using LSCore.DependencyInjection.Models;
using System.Reflection;

namespace LSCore.DependencyInjection;

internal static class Constants
{
    internal static LSCoreDependencyInjectionConfiguration Configuration { get; } = new ();

    public static bool WithDefaultConventions { get; set; } = true;
    public static bool IncludeCallingAssembly { get; set; } = true;
    public static bool IncludeLSCoreDtoMappers { get; set; } = true;
    public static bool IncludeLSCoreValidators { get; set; } = true;
    public static Func<Assembly, bool>? AssemblyAndExecutablesFromApplicationBaseDirectory { get ; set ; }
}