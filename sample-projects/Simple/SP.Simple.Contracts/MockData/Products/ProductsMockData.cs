using SP.Simple.Contracts.Entities;

namespace SP.Simple.Contracts.MockData.Products
{
    public static class ProductsMockData
    {
        public static List<ProductEntity> Products = new List<ProductEntity>();

        public static void SeedData()
        {
            Products.Clear();

            for(int i = 0; i < 100; i++)
            {
                Products.Add(new ProductEntity
                {
                    Id = i,
                    Name = $"Product {i}",
                    Description = $"Description {i}",
                    Price = i * 10,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                });
            }
        }
    }
}
