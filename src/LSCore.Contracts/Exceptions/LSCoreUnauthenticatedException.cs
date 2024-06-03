namespace LSCore.Contracts.Exceptions;

public class LSCoreUnauthenticatedException : Exception
{
    public LSCoreUnauthenticatedException() : base()
    {
    }

    public LSCoreUnauthenticatedException(string message) : base(message)
    {
    }
}
