using LSCore.Contracts.Requests;
using RB.Contracts.Dtos.Products;

namespace RB.Contracts.Interfaces.IManagers;

public interface IProductManager
{
    ProductDto Get(LSCoreIdRequest request);
    List<ProductDto> GetMultiple();
    void Initialize();
}