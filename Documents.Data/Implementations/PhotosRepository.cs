using Azure.Storage.Blobs;
using Documents.Data.Configurations;
using Documents.Data.Interfaces;

namespace Documents.Data.Implementations
{
    public class PhotosRepository : BlobRepository, IPhotosRepository
    {
        public PhotosRepository(BlobServiceClient blobServiceClient, AzuriteConfiguration config)
            : base(blobServiceClient, config, config.PhotosContainerName) { }
    }
}
