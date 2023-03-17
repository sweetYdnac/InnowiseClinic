using Microsoft.Extensions.Configuration;

namespace Documents.Business.Configuration
{
    public class AzuriteConfiguration
    {
        public string AccountName { get; init; }
        public string AccountKey { get; init; }
        public string PhotosContainerName { get; init; }
        public string AppointmentResultsContainerName { get; init; }

        public AzuriteConfiguration(IConfiguration config)
        {
            AccountName = config.GetValue<string>("Azurite:AccountName");
            AccountKey = config.GetValue<string>("Azurite:AccountKey");
            PhotosContainerName = config.GetValue<string>("Azurite:PhotosContainerName");
            AppointmentResultsContainerName = config.GetValue<string>("Azurite:AppointmentResultsContainerName");
        }
    }
}
