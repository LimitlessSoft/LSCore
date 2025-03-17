using LSCore.Validation.Domain;
using Microsoft.AspNetCore.Mvc;
using Sample.Validation.Api.Requests;

namespace Sample.Validation.Api.Controllers;

public class UsersController : ControllerBase
{
	[HttpGet]
	[Route("/users")]
	public IActionResult Get() => Ok("Some data");

	[HttpPost]
	[Route("/register")]
	public IActionResult Register([FromBody] UserRegisterRequest request)
	{
		request.Validate();
		return Ok("User succesfully registered!");
	}
}
