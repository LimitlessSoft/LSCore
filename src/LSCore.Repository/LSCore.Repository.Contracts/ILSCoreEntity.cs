namespace LSCore.Repository.Contracts;

public interface ILSCoreEntity : ILSCoreEntityBase
{
	new long Id { get; set; }
	bool IsActive { get; set; }
	DateTime CreatedAt { get; set; }
	long CreatedBy { get; set; }
	long? UpdatedBy { get; set; }
	DateTime? UpdatedAt { get; set; }
}
