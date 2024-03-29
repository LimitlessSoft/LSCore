﻿using SP.Simple.Contracts.Requests.Products;
using SP.Simple.Contracts.Dtos.Products;
using SP.Simple.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;
using LSCore.Contracts.Http;

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
