using ACR.UsersMicroservice.Client;
using ACR.UsersMicroservice.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ACR.Api.Controllers;

public class UsersController(ACRUsersMicroserviceClient client) : ControllerBase
{
    [HttpGet]
    [Route("/users")]
    public async Task<IActionResult> GetUsers() => Ok(await client.GetMultipleAsync(new GetMultipleRequest()
    {
        MockQueryParam = "mock"
    }));
}