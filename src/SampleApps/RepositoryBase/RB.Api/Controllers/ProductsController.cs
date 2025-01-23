using LSCore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using RB.Contracts.Interfaces.IManagers;

namespace RB.Api.Controllers;

public class ProductsController(IProductManager productManager) : ControllerBase
{
    [HttpPost]
    [Route("/products-initialize")]
    public IActionResult Initialize()
    {
        productManager.Initialize();
        return Ok();
    }

    [HttpGet]
    [Route("/products/{Id}")]
    public IActionResult GetSingle([FromRoute] LSCoreIdRequest request) =>
        Ok(productManager.Get(request));

    [HttpGet]
    [Route("/products")]
    public IActionResult GetMultiple() =>
        Ok(productManager.GetMultiple());
}