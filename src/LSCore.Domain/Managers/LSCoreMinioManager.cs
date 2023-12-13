using LSCore.Contracts.Dtos;
using LSCore.Contracts.Http;
using LSCore.Contracts.SettingsModels;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Tags;

namespace LSCore.Domain.Managers
{
    public class LSCoreMinioManager
    {
        private readonly LSCoreMinioSettings _settings;

        public LSCoreMinioManager(LSCoreMinioSettings settings)
        {
            _settings = settings;
        }

        public  Task UploadAsync(Stream fileStream, string fileName, string contentType, Dictionary<string, string> tags = null) =>
            UploadAsync(new LSCoreMinioUploadOptions()
            {
                Bucket = _settings.BucketBase,
                FileName = fileName,
                FileStream = fileStream,
                ContentType = contentType,
                Tags = tags
            });

        public async Task UploadAsync(LSCoreMinioUploadOptions options)
        {
            var client = new MinioClient()
                .WithEndpoint($"{_settings.Host}:{_settings.Port}")
                .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                .Build();

            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(options.Bucket);

            bool found = await client.BucketExistsAsync(bucketExistsArgs).ConfigureAwait(false);
            if (!found)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(options.Bucket);

                await client.MakeBucketAsync(makeBucketArgs).ConfigureAwait(false);
            }

            var uploadObj = new PutObjectArgs()
                .WithBucket(_settings.BucketBase)
                .WithObjectSize(options.FileName.Length)
            .WithStreamData(options.FileStream)
                .WithObject(options.FileName)
            .WithContentType(options.ContentType);

            if (options.Tags != null)
                uploadObj.WithTagging(new Tagging(options.Tags, false));

            await client.PutObjectAsync(uploadObj).ConfigureAwait(false);
        }

        public Task<LSCoreResponse<LSCoreFileDto>> DownloadAsync(string file) =>
            DownloadAsync(new LSCoreMinioDownloadOptions()
            {
                FileName = file,
                Bucket = _settings.BucketBase
            });

        public async Task<LSCoreResponse<LSCoreFileDto>> DownloadAsync(LSCoreMinioDownloadOptions options)
        {
            var response = new LSCoreResponse<LSCoreFileDto>();
            var client = new MinioClient()
                .WithEndpoint($"{_settings.Host}:{_settings.Port}")
                .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                .Build();

            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(options.Bucket)
                    .WithObject(options.FileName);

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
                .WithObject(options.FileName)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(ms);
                });

            var tagsArgs = new GetObjectTagsArgs()
                .WithBucket(_settings.BucketBase)
                .WithObject(options.FileName);

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
