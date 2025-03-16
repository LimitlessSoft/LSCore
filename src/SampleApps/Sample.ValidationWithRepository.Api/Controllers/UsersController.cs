using LSCore.Validation.Domain;
using Microsoft.AspNetCore.Mvc;
using Sample.ValidationWithRepository.Api.Interfaces;
using Sample.ValidationWithRepository.Api.Requests;

namespace Sample.ValidationWithRepository.Api.Controllers;

public class UsersController(IUserRepository userRepository) : ControllerBase
{
	[HttpGet]
	[Route("/users")]
	public IActionResult Get()
	{
		return Ok("Some data");
	}

	[HttpPost]
	[Route("/register")]
	public IActionResult Register([FromBody] UserRegisterRequest request)
	{
		request.Validate();
		userRepository.Register(request.Username, request.Password); // You shouldn't expose repository layer directly to app, but for simplicity of this example we will
		return Ok("User registered succesfully!");
	}
}
