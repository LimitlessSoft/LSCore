namespace LSCore.Exceptions;

public class LSCoreInternalException : Exception
{
	public LSCoreInternalException()
		: base() { }

	public LSCoreInternalException(string message)
		: base(message) { }
}
