using LSCore.Auth.Contracts;

namespace LSCore.Auth.Key.Contracts;

public class LSCoreAuthKeyConfiguration : LSCoreAuthConfiguration
{
	public required HashSet<string> ValidKeys { get; set; }
}
