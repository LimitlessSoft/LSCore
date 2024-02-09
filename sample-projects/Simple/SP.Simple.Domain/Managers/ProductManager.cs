using SP.Simple.Contracts.Requests.Products;
using SP.Simple.Contracts.MockData.Products;
using SP.Simple.Contracts.Dtos.Products;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Entities;
using LSCore.Domain.Extensions;
using LSCore.Contracts.Http;

namespace SP.Simple.Domain.Managers
{
    public class ProductManager : IProductManager
    {
        public LSCoreListResponse<GetProductsDto> GetMultiple(GetProductsRequest request) =>
            new LSCoreListResponse<GetProductsDto>(ProductsMockData.Products
                .AsQueryable()
                .Where(x => x.IsActive)
                .ToDtoList<GetProductsDto, ProductEntity>());
    }
}
