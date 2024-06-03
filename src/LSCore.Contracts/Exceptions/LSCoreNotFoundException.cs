namespace LSCore.Contracts.Exceptions;

public class LSCoreNotFoundException : Exception
{
    public LSCoreNotFoundException() : base()
    {
        
    }
    
    public LSCoreNotFoundException(string message) : base(message)
    {
    }
}
