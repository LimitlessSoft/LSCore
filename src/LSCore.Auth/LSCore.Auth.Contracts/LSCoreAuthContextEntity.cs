namespace LSCore.Auth.Contracts;

public class LSCoreAuthContextEntity<TEntityIdentifier>
{
	public bool IsAuthenticated => Type != null && Identifier != null;
	public LSCoreAuthEntityType? Type { get; set; }
	public TEntityIdentifier? Identifier { get; set; }
}
