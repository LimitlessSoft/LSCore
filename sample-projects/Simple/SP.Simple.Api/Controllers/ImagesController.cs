using LSCore.Contracts.Http;
using LSCore.Contracts.SettingsModels;
using LSCore.Domain.Managers;
using Microsoft.AspNetCore.Mvc;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Images;

namespace SP.Simple.Api.Controllers
{
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        public ImagesController(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }

        [HttpPost]
        [Route("images")]
        public async Task<LSCoreResponse> Upload([FromForm]ImagesUploadRequest request) =>
            await _imageManager.Upload(request);
    }
}
