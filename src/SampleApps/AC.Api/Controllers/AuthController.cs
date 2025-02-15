using LSCore.Domain.Managers;
using Microsoft.AspNetCore.Mvc;

namespace AC.Api.Controllers;

public class AuthController(LSCoreAuthorizeManager authorizeManager) : ControllerBase
{
    [HttpPost]
    [Route("/auth")]
    public IActionResult Auth([FromBody] AuthRequest request) =>
        Ok(authorizeManager.Authorize(request.Username, request.Password));
}