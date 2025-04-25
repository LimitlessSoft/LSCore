using LSCore.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Sample.AuthKeyProvider.Api.Controllers;

public class ProductsController : ControllerBase
{
	[HttpGet]
	[Route("/products")]
	public IActionResult Get() => Ok("Here is some data");

	[HttpGet]
	[LSCoreAuth]
	[Route("/products-auth")]
	public IActionResult GetAuth() => Ok("Here is some data");
}
