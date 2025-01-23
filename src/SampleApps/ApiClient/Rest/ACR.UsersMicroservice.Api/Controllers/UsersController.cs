using ACR.UsersMicroservice.Dtos;
using ACR.UsersMicroservice.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ACR.UsersMicroservice.Api.Controllers;

public class UsersController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetUsers([FromQuery] GetMultipleRequest request)
    {
        var users = new List<UserDto>
        {
            new()  { Name = "John Doe - " + request.MockQueryParam },
            new()  { Name = "Jane Doe - " + request.MockQueryParam  }
        };
        
        return Ok(users);
    }
}