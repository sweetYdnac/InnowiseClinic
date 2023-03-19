using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Documents.Business.Configuration;
using Documents.Business.Interfaces;
using Shared.Models.Response.Documents;

namespace Documents.Business.Implementations
{
    public abstract class BlobService : IBlobService
    {
        private readonly string _accountName;
        private readonly string _accountKey;
        private readonly string _containerUri;

        public BlobService(
            BlobServiceClient blobServiceClient,
            AzuriteConfiguration config,
            string containerName)
        {
            _accountName = config.AccountName;
            _accountKey = config.AccountKey;

            blobServiceClient
                .GetBlobContainerClient(containerName)
                .CreateIfNotExists();

            _containerUri = blobServiceClient
                .GetBlobContainerClient(containerName).Uri.ToString();
        }

        public async Task<BlobResponse> GetBlobAsync(string name)
        {
            var blobClient = GetBlobClient(name);
            var downloadInfo = await blobClient.DownloadContentAsync();

            var contentType = downloadInfo.Value.Details.ContentType;
            var extension = contentType[(contentType.IndexOf("/") + 1)..];

            return new BlobResponse
            {
                Content = downloadInfo.Value.Content.ToArray(),
                ContentType = downloadInfo.Value.Details.ContentType,
                FileName = $"{name}.{extension}",
            };
        }

        public async Task<Guid> AddOrUpdateBlobAsync(Guid id, string bytes, string contentType)
        {
            var blobClient = GetBlobClient(id.ToString());

            using (var stream = new MemoryStream(Convert.FromBase64String(bytes)))
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType,
                    }
                });

                return id;
            }
        }

        public async Task DeleteBlobAsync(Guid id)
        {
            var blobClient = GetBlobClient(id.ToString());
            await blobClient.DeleteAsync();
        }

        private BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(new Uri($"{_containerUri}/{blobName}"),
                new StorageSharedKeyCredential(_accountName, _accountKey));
        }
    }
}
