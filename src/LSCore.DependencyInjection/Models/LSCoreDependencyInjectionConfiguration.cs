using System.Reflection;

namespace LSCore.DependencyInjection.Models;

internal class LSCoreDependencyInjectionConfiguration
{
    public List<Assembly> AssembliesToBeScanned { get; set; } = [];
}