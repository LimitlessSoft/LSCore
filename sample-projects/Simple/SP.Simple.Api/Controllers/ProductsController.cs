using LSCore.Contracts.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Simple.Contracts.Dtos.Products;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Products;

namespace SP.Simple.Api.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        [Route("/products")]
        public LSCoreListResponse<GetProductsDto> GetMultiple([FromQuery] GetProductsRequest request) =>
            _productManager.GetMultiple(request);
    }
}
