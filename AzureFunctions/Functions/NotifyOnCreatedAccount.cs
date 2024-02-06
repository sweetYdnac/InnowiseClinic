using AzureFunctions.Configurations;
using AzureFunctions.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json.Linq;
using Shared.Messages;
using System.Threading.Tasks;

namespace AzureFunctions.Functions
{
    public class NotifyOnCreatedAccount
    {
        private readonly IEmailSenderService _emailSenderService;
        private readonly EmailServiceConfiguration _configuration;

        public NotifyOnCreatedAccount(IEmailSenderService emailSenderService, IOptions<EmailServiceConfiguration> options) =>
            (_emailSenderService, _configuration) = (emailSenderService, options.Value);

        [FunctionName("NotifyOnCreatedAccount")]
        public async Task Run([RabbitMQTrigger("SendCreateAccountEmail", ConnectionStringSetting = "Values:RabbitConnection")] JObject args)
        {
            var message = args["message"].ToObject<SendCreateAccountEmailMessage>();

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Origin", _configuration.Origin));
            emailMessage.To.Add(new MailboxAddress("Destination", message.Email));
            emailMessage.Subject = "Some subject";
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = $"Your password is - {message.Password}" };

            await _emailSenderService.SendEmailAsync(emailMessage);
        }
    }
}
