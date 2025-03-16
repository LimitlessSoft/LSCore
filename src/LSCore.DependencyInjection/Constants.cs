using System.Reflection;

namespace LSCore.DependencyInjection;

internal static class Constants
{
	internal static LSCoreDependencyInjectionConfiguration Configuration { get; } = new();

	public static bool WithDefaultConventions { get; set; } = true;
	public static bool IncludeCallingAssembly { get; set; } = true;
	public static bool IncludeLSCoreMappers { get; set; } = true;
	public static bool IncludeLSCoreValidators { get; set; } = true;
	public static Func<Assembly, bool>? ShouldScanAssemblyPredicate { get; set; }
}
