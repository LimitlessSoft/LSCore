using Microsoft.AspNetCore.Mvc;
using Sample.SortAndPage.Api.Interfaces;
using Sample.SortAndPage.Api.Requests;

namespace Sample.SortAndPage.Api.Controllers;

public class ProductsController(IProductManager productManager) : ControllerBase
{
	[HttpGet]
	[Route("/products")]
	public IActionResult GetAll([FromQuery] ProductsGetAllRequest request) =>
		Ok(productManager.GetAll(request));
}
