using SP.Simple.Contracts.Entities;
using SP.Simple.Contracts.Interfaces;

namespace SP.Simple.Repository
{
    // This is mock repository with InMemory database
    // This should not be used in production
    // Please refere to implementation of LSCore.Repository for real database
    public class SimpleDbContext : ISimpleDbContext
    {
        public IEnumerable<ProductEntity> Products { get; private set; } = new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = 1,
                    Name = "Product 1",
                    IsActive = true
                },
                new ProductEntity
                {
                    Id = 2,
                    Name = "Product 2",
                    IsActive = true
                },
                new ProductEntity
                {
                    Id = 3,
                    Name = "Product 3",
                    IsActive = true
                }
            };
    }
}
