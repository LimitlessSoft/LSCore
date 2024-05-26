using Microsoft.AspNetCore.Mvc;

namespace Sample.Minimal.Api.Controllers;

[ApiController]
public class PingController : ControllerBase
{
    public PingController()
    {
        
    }
    
    [HttpGet("/ping")]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }
}