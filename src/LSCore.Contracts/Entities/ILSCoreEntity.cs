namespace LSCore.Contracts.Entities
{
    public interface ILSCoreEntity : ILSCoreEntityBase
    {
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
