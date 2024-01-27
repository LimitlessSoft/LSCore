using LSCore.Contracts.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Simple.Contracts.IManagers;

namespace SP.Simple.Api.Controllers
{
    /// <summary>
    /// Controller used as test
    /// </summary>
    [ApiController]
    public class PingController : ControllerBase
    {
        private readonly IPingManager _pingManager;

        /// <summary>
        /// Default controller constructor
        /// </summary>
        /// <param name="pingManager"></param>
        public PingController(IPingManager pingManager)
        {
            _pingManager = pingManager;
        }

        /// <summary>
        /// Used as test EP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/ping")]
        public LSCoreResponse PingGet() =>
            _pingManager.GetPing();
    }
}
