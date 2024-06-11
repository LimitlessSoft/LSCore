namespace LSCore.Contracts.Requests;

public class LSCoreSaveRequest
{
    public long? Id { get; set; }

    public LSCoreSaveRequest()
    {

    }

    public LSCoreSaveRequest(long? Id)
    {
        this.Id = Id;
    }

    public bool IsNew => !Id.HasValue;
    public bool IsOld => Id.HasValue;
}