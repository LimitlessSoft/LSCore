using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Sample.AuthorizationSimple.Api.Controllers;

public class ProductsControllers : ControllerBase
{
    [HttpGet]
    [Route("/products")]
    public IActionResult GetProducts()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Product 1" },
            new { Id = 2, Name = "Product 2" },
            new { Id = 3, Name = "Product 3" }
        });
    }
    
    [HttpGet]
    [LSCoreAuthorize] // Simply add this attribute to protect the endpoint so only authorized users can access it
    [Route("/products-auth")]
    public IActionResult GetProductsAuth()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Product 1" },
            new { Id = 2, Name = "Product 2" },
            new { Id = 3, Name = "Product 3" }
        });
    }
}