namespace LSCore.Auth.Contracts;

public interface ILSCoreAuthEntity<out TEntityIdentifier>
{
	public TEntityIdentifier Identifier { get; }
}
