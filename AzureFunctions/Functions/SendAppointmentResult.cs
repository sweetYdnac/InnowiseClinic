using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AzureFunctions.Functions
{
    public class SendAppointmentResult
    {
        [FunctionName("SendAppointmentResult")]
        public void Run([BlobTrigger("appointment-results/{name}", Connection = "AzuriteConnection")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
