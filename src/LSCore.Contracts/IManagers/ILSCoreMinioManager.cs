using LSCore.Contracts.Dtos;
using LSCore.Contracts.Http;
using LSCore.Contracts.SettingsModels;

namespace LSCore.Contracts.IManagers
{
    public interface ILSCoreMinioManager
    {
        Task UploadAsync(Stream fileStream, string fileName, string contentType, Dictionary<string, string>? tags = null);
        Task UploadAsync(LSCoreMinioUploadOptions options);
        Task<LSCoreResponse<LSCoreFileDto>> DownloadAsync(string file);
        Task<LSCoreResponse<LSCoreFileDto>> DownloadAsync(LSCoreMinioDownloadOptions options);
    }
}
