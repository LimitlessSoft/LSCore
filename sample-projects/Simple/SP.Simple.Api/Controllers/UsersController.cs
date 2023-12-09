using LSCore.Contracts.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Users;

namespace SP.Simple.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _userManager = userManager;
            _userManager.SetContext(_httpContextAccessor.HttpContext!);
        }

        [HttpPost]
        [Route("/login")]
        public LSCoreResponse<string> Login([FromBody] LoginUserRequest request) =>
            _userManager.Login(request);

        /// <summary>
        /// Returns username from token. Store user id / username which is associated to your other user data stored in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("/me")]
        public LSCoreResponse<string> Me() =>
            _userManager.Me();
    }
}
