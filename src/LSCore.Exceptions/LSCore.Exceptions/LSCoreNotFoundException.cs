namespace LSCore.Exceptions;

public class LSCoreNotFoundException : Exception
{
	public LSCoreNotFoundException()
		: base() { }

	public LSCoreNotFoundException(string message)
		: base(message) { }
}
