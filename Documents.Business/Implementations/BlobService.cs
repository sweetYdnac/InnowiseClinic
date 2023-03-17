using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Documents.Business.Configuration;
using Documents.Business.Interfaces;
using MimeDetective;
using Shared.Models.Response.Documents;

namespace Documents.Business.Implementations
{
    public abstract class BlobService : IBlobService
    {
        private readonly ContentInspector _inspector;

        private readonly string _accountName;
        private readonly string _accountKey;
        private readonly string _containerUri;

        public BlobService(
            BlobServiceClient blobServiceClient,
            AzuriteConfiguration config,
            string containerName)
        {
            _inspector = new ContentInspectorBuilder()
            {
                Definitions = MimeDetective.Definitions.Default.All()
            }.Build();

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

            return new BlobResponse
            {
                Content = downloadInfo.Value.Content.ToArray(),
                ContentType = downloadInfo.Value.Details.ContentType,
                FileName = name,
            };
        }

        public async Task<Guid> UploadBlobAsync(Guid id, string bytes)
        {
            var blobClient = GetBlobClient(id.ToString());
            //await blobClient.UploadAsync(BinaryData.FromString(bytes), new BlobUploadOptions
            //{
            //    HttpHeaders =
            //    {
            //        //ContentType = GetContentType(bytes),
            //        ContentType = "application/pdf"
            //    }
            //});

            await blobClient.UploadAsync(BinaryData.FromString(bytes));

            return id;
        }

        public async Task DeleteBlobAsync(Guid id)
        {
            var blobClient = GetBlobClient(id.ToString());
            await blobClient.DeleteAsync();
        }

        public async Task UpdateBlobAsync(Guid id, string bytes)
        {
            var blobClient = GetBlobClient(id.ToString());

            using (var stream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(stream);
                stream.Position = 0;
                stream.Write(BinaryData.FromString(bytes).ToArray(), 0, bytes.Length);

                await blobClient.UploadAsync(stream);
            }
        }

        protected BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(new Uri($"{_containerUri}/{blobName}"),
                new StorageSharedKeyCredential(_accountName, _accountKey));
        }

        private string GetContentType(string bytes)
        {
            var array = System.Text.Encoding.UTF8.GetBytes(bytes);

            return _inspector.Inspect(bytes).FirstOrDefault().Definition.File.MimeType;
        }
    }
}
