using LSCore.Contracts.Http;
using SP.Simple.Contracts.Requests.Images;

namespace SP.Simple.Contracts.IManagers
{
    public interface IImageManager
    {
        Task<LSCoreResponse> Upload(ImagesUploadRequest request);
    }
}
