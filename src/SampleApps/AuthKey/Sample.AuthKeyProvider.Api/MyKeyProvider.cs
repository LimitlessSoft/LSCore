using LSCore.Auth.Key.Contracts;

namespace Sample.AuthKeyProvider.Api;

public class MyKeyProvider : ILSCoreAuthKeyProvider
{
	public bool IsValidKey(string key)
	{
		// Inject database, configuration or any other way to load valid keys and validate currently requested key
		return true;
	}
}
