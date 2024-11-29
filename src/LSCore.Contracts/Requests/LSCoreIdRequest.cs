namespace LSCore.Contracts.Requests;

public class LSCoreIdRequest
{
    public long Id { get; set; }
    
    public LSCoreIdRequest()
    {
        
    }
    
    public LSCoreIdRequest(long id)
    {
        Id = id;
    }
}
