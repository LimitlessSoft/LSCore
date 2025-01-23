using LSCore.Contracts.Interfaces.Repositories;
using RB.Contracts.Entities;

namespace RB.Contracts.Interfaces.IRepositories;

public interface IProductRepository : ILSCoreRepositoryBase<ProductEntity>
{
    void Create(List<ProductEntity> products);
}