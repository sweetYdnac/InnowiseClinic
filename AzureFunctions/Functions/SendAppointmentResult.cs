using AzureFunctions.Configurations;
using AzureFunctions.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctions.Functions
{
    public class SendAppointmentResult
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly EmailServiceConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public SendAppointmentResult(
            IEmailSenderService emailSenderService,
            IOptions<EmailServiceConfiguration> options,
            IHttpClientFactory httpClientFactory) =>
        (_emailSenderService, _configuration, _httpClientFactory) = (emailSenderService, options.Value, httpClientFactory);

        [FunctionName("SendAppointmentResult")]
        public async Task Run([BlobTrigger("appointment-results/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string appointmentResultId, ILogger log)
        {
            //var client = _httpClientFactory.CreateClient();

            //var request = new ClientCredentialsTokenRequest
            //{
            //    Address = "http://host.docker.internal:8020/connect/token",
            //    ClientId = "machineClient",
            //    Scope = "Full"
            //};

            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(request);

            //var apiClient = new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            //var appointmentResult = await apiClient.GetUserInfoAsync(new UserInfoRequest
            //{
            //    Address = "http://host.docker.internal:8020/userinfo",
            //    ClientCredentialStyle = ClientCredentialStyle.AuthorizationHeader
            //});

            //var response = await apiClient.GetAsync($"http://host.docker.internal:8020/Account/{}");

            //if (response.IsSuccessStatusCode)
            //{
            //    var account = response.Content;
            //}

            //log.LogInformation($"C# Blob trigger function Processed blob\n Name:{appointmentResultId} \n Size: {myBlob.Length} Bytes");

            // get email


            //var emailMessage = new MimeMessage();
            //emailMessage.From.Add(new MailboxAddress("Origin", _configuration.Origin));
            //emailMessage.To.Add(new MailboxAddress("Destination", message.Email));
            //emailMessage.Subject = "Some subject";
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = $"Your password is - {message.Password}" };

            //await _emailSenderService.SendEmailAsync(emailMessage);
        }
    }
}
