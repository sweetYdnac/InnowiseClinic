using AzureFunctions.Configurations;
using AzureFunctions.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace AzureFunctions.Services.Implementations
{
    public class EtherealEmailNotifierService : IEmailSenderService
    {
        private readonly EmailServiceConfiguration _configuration;

        public EtherealEmailNotifierService(IOptions<EmailServiceConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task SendEmailAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.Host, _configuration.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_configuration.UserName, _configuration.Password);

                    await client.SendAsync(emailMessage);
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
