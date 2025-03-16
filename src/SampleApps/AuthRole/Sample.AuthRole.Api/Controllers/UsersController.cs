using LSCore.Auth.Contracts;
using LSCore.Auth.Role.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Microsoft.AspNetCore.Mvc;
using Sample.AuthRole.Api.Enums;
using Sample.AuthRole.Api.Requests;

namespace Sample.AuthRole.Api.Controllers;

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
	[Route("/users-auth-any-role")]
	public IActionResult GetAnyRole() => Ok("Here is some auth data");

	[HttpGet]
	[LSCoreAuthRole<UserRole>(UserRole.Administrator)]
	[Route("/users-auth-administrator-role")]
	public IActionResult GetAdministratorRole() => Ok("Here is some auth data");
}
