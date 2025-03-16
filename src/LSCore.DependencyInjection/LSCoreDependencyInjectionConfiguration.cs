using System.Reflection;

namespace LSCore.DependencyInjection;

internal class LSCoreDependencyInjectionConfiguration
{
	public List<Assembly> AssembliesToBeScanned { get; set; } = [];
}
