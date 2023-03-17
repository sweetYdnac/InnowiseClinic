using Azure.Storage.Blobs;
using Documents.Business.Configuration;
using Documents.Business.Interfaces;

namespace Documents.Business.Implementations
{
    public class PhotoService : BlobService, IPhotoService
    {
        public PhotoService(BlobServiceClient blobServiceClient, AzuriteConfiguration config)
            : base(blobServiceClient, config, config.PhotosContainerName) { }
    }
}
