﻿using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Documents.Data.Configurations;
using Documents.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response.Documents;

namespace Documents.Data.Implementations
{
    public class BlobRepository : IBlobRepository
    {

        private readonly AzuriteConfiguration _config;
        private readonly string _containerUri;

        public BlobRepository(
            BlobServiceClient blobServiceClient,
            AzuriteConfiguration config,
            string containerName)
        {
            _config = config;

            blobServiceClient
                .GetBlobContainerClient(containerName)
                .CreateIfNotExists();

            _containerUri = blobServiceClient
                .GetBlobContainerClient(containerName).Uri.ToString();
        }

        public async Task<BlobResponse> GetBlobAsync(Guid id)
        {
            var blobClient = GetBlobClient(id.ToString());

            try
            {
                var downloadInfo = await blobClient.DownloadContentAsync();
                var contentType = downloadInfo.Value.Details.ContentType;
                var extension = contentType[(contentType.IndexOf("/") + 1)..];

                return new BlobResponse
                {
                    Content = downloadInfo.Value.Content.ToArray(),
                    ContentType = downloadInfo.Value.Details.ContentType,
                    FileName = $"{id}.{extension}",
                };
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                throw new NotFoundException($"Blob with name = {id} doesn't exist.");
            }
        }

        public async Task<Guid> AddOrUpdateBlobAsync(Guid id, Stream stream, string contentType)
        {
            var blobClient = GetBlobClient(id.ToString());

            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = contentType,
                }
            });

            return id;
        }

        public async Task DeleteBlobAsync(Guid id)
        {
            var blobClient = GetBlobClient(id.ToString());
            await blobClient.DeleteAsync();
        }

        private BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(new Uri($"{_containerUri}/{blobName}"),
                new StorageSharedKeyCredential(_config.AccountName, _config.AccountKey));
        }
    }
}
