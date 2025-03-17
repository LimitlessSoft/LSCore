namespace LSCore.Common.Contracts;

public class LSCoreIdRequest
{
	public long Id { get; set; }

	public LSCoreIdRequest() { }

	public LSCoreIdRequest(long id) => Id = id;
}
