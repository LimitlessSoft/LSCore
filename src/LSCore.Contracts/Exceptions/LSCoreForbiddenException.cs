namespace LSCore.Contracts.Exceptions;

public class LSCoreForbiddenException : Exception
{
    public LSCoreForbiddenException() : base()
    {
    }

    public LSCoreForbiddenException(string message) : base(message)
    {
    }
}