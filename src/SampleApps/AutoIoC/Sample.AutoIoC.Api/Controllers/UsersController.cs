using Microsoft.AspNetCore.Mvc;
using Sample.AutoIoC.Contracts.Interfaces.IManagers;

namespace Sample.AutoIoC.Api.Controllers;

public class UsersController (IUserManager userManager) : ControllerBase
{
    [Route("/users")]
    public IActionResult Get() =>
        Ok(userManager.GetUser());
}