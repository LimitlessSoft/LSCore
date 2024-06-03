using Sample.Minimal.Contracts.IManagers;
using LSCore.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Minimal.Api.Controllers;

[ApiController]
public class UsersController (IUserManager userManager)
    : ControllerBase
{
    [HttpGet]
    [Route("/users")]
    public IActionResult GetUsers() =>
        Ok(userManager.GetMultiple());
    
    [HttpGet]
    [Route("/users/{Id}")]
    public IActionResult GetUser([FromRoute] LSCoreIdRequest request) =>
        Ok(userManager.GetSingular(request));
}