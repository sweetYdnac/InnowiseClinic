using AzureFunctions.Configurations;
using AzureFunctions.Models;
using AzureFunctions.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureFunctions.Functions
{
    public class SendAppointmentResult
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        private readonly ITokenService _tokenService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly EmailServiceConfiguration _emailConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public SendAppointmentResult(
            ITokenService tokenService,
            IEmailSenderService emailSenderService,
            IOptions<EmailServiceConfiguration> emailOptions,
            IHttpClientFactory httpClientFactory) =>
        (_tokenService, _emailSenderService, _emailConfig, _httpClientFactory) =
        (tokenService, emailSenderService, emailOptions.Value, httpClientFactory);

        [FunctionName("SendAppointmentResult")]
        public async Task Run([BlobTrigger("appointment-results/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string name)
        {
            var tokenResponse = await _tokenService.GetTokenAsync();

            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var appointmentResponse = await apiClient.GetAsync($"https://localhost:10001/appointments/{name}");
            appointmentResponse.EnsureSuccessStatusCode();

            using (var stream = await appointmentResponse.Content.ReadAsStreamAsync())
            {
                var appointment = await JsonSerializer.DeserializeAsync<AppointmentResponse>(stream, _options);

                var accountResponse = await apiClient.GetAsync($"https://localhost:10001/authorization/{appointment.PatientId}");
                accountResponse.EnsureSuccessStatusCode();

                using (var accountStream = await accountResponse.Content.ReadAsStreamAsync())
                {
                    var account = await JsonSerializer.DeserializeAsync<AccountResponse>(accountStream, _options);

                    var emailMessage = new MimeMessage();
                    emailMessage.From.Add(new MailboxAddress("Origin", _emailConfig.Origin));
                    emailMessage.To.Add(new MailboxAddress("Destination", account.Email));
                    emailMessage.Subject = "Your appointment result";
                    emailMessage.Body = new TextPart(TextFormat.Text) { Text = $"Hello, this is your appointment result" };

                    var attachment = new MimePart("application/pdf")
                    {
                        Content = new MimeContent(myBlob),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = $"Appointment_result_{DateTime.Now:yyyy-MM-dd}"
                    };

                    emailMessage.Body = new Multipart("mixed") { emailMessage.Body, attachment };

                    await _emailSenderService.SendEmailAsync(emailMessage);
                }
            }
        }
    }
}
