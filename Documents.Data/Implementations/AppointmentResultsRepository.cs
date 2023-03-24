using Azure.Storage.Blobs;
using Documents.Data.Configurations;
using Documents.Data.Interfaces;

namespace Documents.Data.Implementations
{
    public class AppointmentResultsRepository : BlobRepository, IAppointmentResultsRepository
    {
        public AppointmentResultsRepository(BlobServiceClient blobServiceClient, AzuriteConfiguration config)
            : base(blobServiceClient, config, config.AppointmentResultsContainerName) { }
    }
}
