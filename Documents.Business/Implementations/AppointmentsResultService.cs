using Azure.Storage.Blobs;
using Documents.Business.Configuration;
using Documents.Business.Interfaces;

namespace Documents.Business.Implementations
{
    public class AppointmentsResultService : BlobService, IAppointmentResultsService
    {
        public AppointmentsResultService(BlobServiceClient blobServiceClient, AzuriteConfiguration config)
            : base(blobServiceClient, config, config.AppointmentResultsContainerName) { }
    }
}
