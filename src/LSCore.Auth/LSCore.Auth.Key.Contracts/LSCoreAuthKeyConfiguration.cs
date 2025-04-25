using LSCore.Auth.Contracts;

namespace LSCore.Auth.Key.Contracts;

public class LSCoreAuthKeyConfiguration : LSCoreAuthConfiguration
{
	[Obsolete(
		"This property is deprecated and used only for backward compatibility. "
			+ "Use builder.AddLSCoreAuthKey<T> which doesn't use this property at all."
	)]
	public HashSet<string>? ValidKeys { get; init; }
}
