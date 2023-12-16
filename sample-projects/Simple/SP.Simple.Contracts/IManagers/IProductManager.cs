using LSCore.Contracts.Http;
using SP.Simple.Contracts.Dtos.Products;
using SP.Simple.Contracts.Requests.Products;

namespace SP.Simple.Contracts.IManagers
{
    public interface IProductManager
    {
        LSCoreListResponse<GetProductsDto> GetMultiple(GetProductsRequest request);
    }
}
