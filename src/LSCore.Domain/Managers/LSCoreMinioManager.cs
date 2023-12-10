using LSCore.Contracts;
using LSCore.Contracts.Dtos;
using LSCore.Contracts.Http;
using LSCore.Contracts.SettingsModels;
using Minio;
using Minio.DataModel.Args;
using static LSCore.Contracts.LSCoreContractsConstants;

namespace LSCore.Domain.Managers
{
    public class LSCoreMinioManager
    {
        private readonly LSCoreMinioSettings _settings;

        public LSCoreMinioManager(LSCoreMinioSettings settings)
        {
            _settings = settings;
        }

        public async Task UploadAsync(Stream fileStream, string fileName, string contentType, Dictionary<string, string> tags = null)
        {
            fileName = fileName.Replace(Path.DirectorySeparatorChar, LSCoreContractsConstants.Minio.DictionarySeparatorChar);
            if (fileName[0] != LSCoreContractsConstants.Minio.DictionarySeparatorChar)
                fileName = LSCoreContractsConstants.Minio.DictionarySeparatorChar + fileName;

            var client = new MinioClient()
                .WithEndpoint($"{_settings.Host}:{_settings.Port}")
                .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                .Build();

            var be = new BucketExistsArgs()
                .WithBucket(_settings.BucketBase);

            bool found = await client.BucketExistsAsync(be).ConfigureAwait(false);
            if (!found)
            {
                var mb = new MakeBucketArgs()
                    .WithBucket(_settings.BucketBase);

                await client.MakeBucketAsync(mb).ConfigureAwait(false);
            }

            var uploadObj = new PutObjectArgs()
                .WithBucket(_settings.BucketBase)
                .WithObjectSize(fileStream.Length)
                .WithStreamData(fileStream)
                .WithObject(fileName)
                .WithContentType(contentType);

            if (tags != null)
                uploadObj.WithTagging(new Minio.DataModel.Tags.Tagging(tags, false));

            await client.PutObjectAsync(uploadObj).ConfigureAwait(false);
        }

        public async Task<LSCoreResponse<LSCoreFileDto>> DownloadAsync(string file)
        {
            file = file.Replace(Path.DirectorySeparatorChar, LSCoreContractsConstants.Minio.DictionarySeparatorChar);

            var response = new LSCoreResponse<LSCoreFileDto>();
            var client = new MinioClient()
                .WithEndpoint($"{_settings.Host}:{_settings.Port}")
                .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                .Build();

            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(_settings.BucketBase)
                    .WithObject(file);

                await client.StatObjectAsync(statObjectArgs).ConfigureAwait(false);
            }
            catch
            {
                response.Status = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var ms = new MemoryStream();
            var getArgs = new GetObjectArgs()
                .WithBucket(_settings.BucketBase)
                .WithObject(file)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(ms);
                });

            var tagsArgs = new GetObjectTagsArgs()
                .WithBucket(_settings.BucketBase)
                .WithObject(file);

            var r = await client.GetObjectAsync(getArgs).ConfigureAwait(false);
            var tags = await client.GetObjectTagsAsync(tagsArgs).ConfigureAwait(false);

            response.Payload = new LSCoreFileDto()
            {
                Data = ms.ToArray(),
                ContentType = r.ContentType,
                Tags = tags == null || tags.Tags == null ? new Dictionary<string, string>() : new Dictionary<string, string>(tags.Tags)
            };
            return response;
        }
    }
}
