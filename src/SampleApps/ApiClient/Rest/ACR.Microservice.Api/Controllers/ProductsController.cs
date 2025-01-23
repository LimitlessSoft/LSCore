using ACR.Microservice.Contracts.Dtos.Products;
using ACR.Microservice.Contracts.Requests.Products;
using Microsoft.AspNetCore.Mvc;

namespace ACR.Microservice.Api.Controllers;

public class ProductsController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetProducts([FromQuery] GetMultipleRequest request) =>
        Ok(new List<ProductDto>
        {
            new () { Name = $"Product 1 - {request.MockQueryParam}" },
            new () { Name = $"Product 2 - {request.MockQueryParam}" }
        });
}