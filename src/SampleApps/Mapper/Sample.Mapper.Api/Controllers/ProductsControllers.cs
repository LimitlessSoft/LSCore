using Microsoft.AspNetCore.Mvc;
using Sample.Mapper.Api.Interfaces;

namespace Sample.Mapper.Api.Controllers;

public class ProductsControllers(IProductManager productManager) : ControllerBase
{
	[HttpGet]
	[Route("/products")]
	public IActionResult GetMultiple() => Ok(productManager.GetAll());

	[HttpGet]
	[Route("/products/{id:int}")]
	public IActionResult GetSingle([FromRoute] int id) => Ok(productManager.Get(id));
}
