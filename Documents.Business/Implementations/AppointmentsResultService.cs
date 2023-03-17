using Azure.Storage.Blobs;
using Documents.Business.Configuration;
using Documents.Business.Interfaces;

namespace Documents.Business.Implementations
{
    public class AppointmentsResultService : BlobService, IAppointmentResultsService
    {
        public AppointmentsResultService(BlobServiceClient blobServiceClient, AzuriteConfiguration config)
            : base(blobServiceClient, config, config.AppointmentResultsContainerName) { }

        public async Task UpdateOrCreateAsync(Guid id, string bytes)
        {
            var blobClient = GetBlobClient(id.ToString());

            if (await blobClient.ExistsAsync())
            {
                await UpdateBlobAsync(id, bytes);
            }
            else
            {
                await UploadBlobAsync(id, bytes);
            }
        }
    }
}
