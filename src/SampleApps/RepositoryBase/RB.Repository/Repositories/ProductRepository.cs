using LSCore.Repository;
using RB.Contracts.Entities;
using RB.Contracts.Interfaces.IRepositories;

namespace RB.Repository.Repositories;

public class ProductRepository(RBDbContext dbContext) :
    LSCoreRepositoryBase<ProductEntity>(dbContext), IProductRepository
{
    public void Create(List<ProductEntity> products)
    {
        dbContext.Products.AddRange(products);
        dbContext.SaveChanges();
    }
}