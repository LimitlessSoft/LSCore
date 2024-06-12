using LSCore.Contracts.Interfaces;

namespace LSCore.Contracts.Entities;

public class LSCoreEntity : ILSCoreEntity
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}