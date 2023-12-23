using LSCore.Contracts.Http;
using LSCore.Contracts.IManagers;
using SP.Simple.Contracts.IManagers;
using SP.Simple.Contracts.Requests.Images;

namespace SP.Simple.Domain.Managers
{
    public class ImageManager : IImageManager
    {
        private readonly ILSCoreMinioManager _minioManager;
        public ImageManager(ILSCoreMinioManager minioManager)
        {
            _minioManager = minioManager;
        }

        public async Task<LSCoreResponse> Upload(ImagesUploadRequest request)
        {
            try
            {
                await _minioManager.UploadAsync(request.Image.OpenReadStream(), request.Image.FileName, request.Image.ContentType, null);
            }
            catch
            {
                return LSCoreResponse.BadRequest();
            }
            return new LSCoreResponse();
        }
    }
}
