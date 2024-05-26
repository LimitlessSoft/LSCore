namespace LSCore.Contracts.Exceptions;

public class LSCoreNotFoundException : Exception
{
    public LSCoreNotFoundException()
    {
        
    }
    
    public LSCoreNotFoundException(string message)
        : base(message)
    {
    }
}