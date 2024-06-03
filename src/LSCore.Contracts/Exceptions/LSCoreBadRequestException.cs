namespace LSCore.Contracts.Exceptions;

public class LSCoreBadRequestException : Exception
{
    public LSCoreBadRequestException() : base()
    {
    }

    public LSCoreBadRequestException(string message) : base(message)
    {
    }
}
