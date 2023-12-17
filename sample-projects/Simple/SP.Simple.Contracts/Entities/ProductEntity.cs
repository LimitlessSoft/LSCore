using LSCore.Contracts.Entities;

namespace SP.Simple.Contracts.Entities
{
    public class ProductEntity : LSCoreEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
