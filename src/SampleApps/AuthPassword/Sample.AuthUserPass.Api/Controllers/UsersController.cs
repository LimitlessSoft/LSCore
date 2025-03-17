using LSCore.Auth.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Sample.AuthUserPass.Api.Controllers;

public class UsersController(ILSCoreAuthPasswordManager<string> authPasswordManager)
	: ControllerBase
{
	[HttpPost]
	[Route("/login")]
	public IActionResult Login([FromBody] LoginRequest request) =>
		Ok(authPasswordManager.Authenticate(request.Username, request.Password));

	[HttpGet]
	[Route("/users")]
	public IActionResult Get() => Ok("Here is some data");

	[HttpGet]
	[LSCoreAuth]
	[Route("/users-auth")]
	public IActionResult GetAuth() => Ok("Here is some auth data");
}
