using SP.Playground.Contracts.Dtos.Users;
using SP.Playground.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;
using LSCore.Contracts.Http;
using LSCore.Contracts.Responses;
using SP.Playground.Contracts.Requests.Users;

namespace SP.Playground.Api.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/users")]
        public LSCoreListResponse<UsersGetDto> GetMultiple() =>
            _userManager.GetMultiple();

        [HttpGet]
        [Route("/users-sorted-and-paged")]
        public LSCoreSortedPagedResponse<UsersGetDto> GetMultipleSortedAndPaged([FromQuery] UsersGetMultipleSortedAndPagedRequest request) =>
            _userManager.GetMultipleSortedAndPaged(request);
    }
}
