using SP.Simple.Contracts.Requests.Images;
using SP.Simple.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;
using LSCore.Contracts.Http;

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

        /// <summary>
        /// Uploads an image to the minio server
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("images")]
        public async Task<LSCoreResponse> Upload([FromForm]ImagesUploadRequest request) =>
            await _imageManager.Upload(request);
    }
}
