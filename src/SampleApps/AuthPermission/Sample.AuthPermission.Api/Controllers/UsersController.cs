using LSCore.Auth.Contracts;
using LSCore.Auth.Permission.Contracts;
using LSCore.Auth.UserPass.Contracts;
using Microsoft.AspNetCore.Mvc;
using Sample.AuthPermission.Api.Enums;
using Sample.AuthPermission.Api.Requests;

namespace Sample.AuthPermission.Api.Controllers;

public class UsersController(ILSCoreAuthUserPassManager<string> authPasswordManager)
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
	[LSCoreAuthPermission<UserPermission>(
		UserPermission.Permission_One,
		UserPermission.Permission_Two
	)]
	[Route("/users-auth-all-permissions")]
	public IActionResult GetHasPermissions() =>
		Ok("User must have all of the permissions to see this");

	[HttpGet]
	[LSCoreAuthPermission<UserPermission>(
		UserPermission.Permission_One,
		UserPermission.Permission_Two,
		UserPermission.Permission_Three
	)]
	[Route("/users-auth-no-permission")]
	public IActionResult GetNoPermission() =>
		Ok("User must have all of the permissions to see this");

	[HttpGet]
	[LSCoreAuthPermission<UserPermission>(
		false,
		UserPermission.Permission_One,
		UserPermission.Permission_Two,
		UserPermission.Permission_Three
	)]
	[Route("/users-auth-any-permission")]
	public IActionResult GetAnyRole() => Ok("User must have any of the permissions to see this");
}
