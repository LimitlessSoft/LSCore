using LSCore.Contracts.Interfaces;

namespace LSCore.Contracts.Entities
{
    public class LSCoreEntity : ILSCoreEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
