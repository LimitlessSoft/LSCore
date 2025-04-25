using LSCore.Auth.Key.Contracts;

namespace LSCore.Auth.Key.DependencyInjection;

/// <summary>
/// Used as a dummy class for backward compatibility with ValidKeys.
/// </summary>
internal class DummyLSCoreAuthKeyProvider : ILSCoreAuthKeyProvider
{
	public bool IsValidKey(string key) => false;
}
