namespace LSCore.Auth.Key.Contracts;

public interface ILSCoreAuthKeyProvider
{
	bool IsValidKey(string key);
}
