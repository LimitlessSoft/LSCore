using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AC.Api.Controllers;

public class PingController : ControllerBase
{
    [HttpGet]
    [Route("/ping")]
    public string Ping() => "Pong";
    
    [HttpGet]
    [LSCoreAuthorize]
    [Route("/ping-auth")]
    public string PingAuth() => "Pong Auth";
}