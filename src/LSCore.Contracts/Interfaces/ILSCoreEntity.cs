namespace LSCore.Contracts.Interfaces
{
    public interface ILSCoreEntity : ILSCoreEntityBase
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}